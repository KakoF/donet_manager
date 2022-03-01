using Xunit;
using Moq;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Service.Services;
using AutoMapper;
using Domain.DTO.Usuario;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Exceptions;

namespace Services.UnitTests
{
    public class UsuarioServiceTests
    {
        private readonly UsuarioService _sut;


        private readonly MockRepository _mockRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<IRedisIntegrator> _mockRedisIntegrator = new Mock<IRedisIntegrator>();
        
        IMapper _mapper;

        public UsuarioServiceTests()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockUsuarioRepository = _mockRepository.Create<IUsuarioRepository>();
            _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();
            _mockRedisIntegrator = _mockRepository.Create<IRedisIntegrator>();
            _sut = new UsuarioService(_mockUsuarioRepository.Object, _mapper, _mockUnitOfWork.Object, _mockRedisIntegrator.Object);

        }

        [Fact]
        public async void GetAsync_ValidId_ReturnCachedUser()
        {
            //Arr
            int id = It.IsAny<int>();
            UsuarioDto UsuarioDto = new UsuarioDto() 
            { 
                Id = id, 
                Nome = "Marcos", 
                Email= "kakoferrare@gmail.com" 
            };
            _mockRedisIntegrator.Setup(c => c.GetAsync<UsuarioDto>($"Usuario_{id}")).ReturnsAsync(UsuarioDto);

            //Act
            var result = await _sut.ReadAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.ReadAsync(id), Times.Never);

            Assert.Equal(result.Id, id);
            Assert.True(result.Equals(_mapper.Map<UsuarioDto>(UsuarioDto)));

        }

        [Fact]
        public async void GetAsync_ValidId_ReturnUser()
        {
            //Arr
            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);
            _mockUsuarioRepository.Setup(c => c.ReadAsync(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.ReadAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.ReadAsync(id), Times.Once);

            Assert.Equal(result.Id, id);
            Assert.True(result.Equals(_mapper.Map<UsuarioDto>(entity)));

        }


        [Fact]
        public async void GetAsync_ReturnListUsers()
        {
            //Arr
            var entitys = new List<Usuario>()
            {
                new Usuario(1, "Marcos", "marcosferrare@gmail.com", DateTime.Now, null),
                new Usuario(2, "Kako", "kakoferrare@gmail.com", DateTime.Now, null),
            };
            _mockUsuarioRepository.Setup(c => c.ReadAsync()).ReturnsAsync(entitys);

            //Act
            var result = await _sut.ReadAsync();

            //Assert
            _mockUsuarioRepository.Verify(c => c.ReadAsync(), Times.Once);
            Assert.Equal(result.Count(), _mapper.Map<List<UsuarioDto>>(entitys).Count);
        }

        [Fact]
        public async void DeleteAsync_ValidId_ReturnTrue()
        {
            //Arr
            int id = It.IsAny<int>();
            _mockUsuarioRepository.Setup(c => c.DeleteAsync(id)).ReturnsAsync(true);

            //Act
            var result = await _sut.DeleteAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.DeleteAsync(id), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async void DeleteAsync_InvalidId_ReturnFalse()
        {
            //Arr
            int id = It.IsAny<int>();
            _mockUsuarioRepository.Setup(c => c.DeleteAsync(id)).ReturnsAsync(false);

            //Act
            var result = await _sut.DeleteAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.DeleteAsync(id), Times.Once);
            Assert.False(result);
        }

        [Fact]
        public void CreateAsync_EmptyStringNome_ReturnDomainException()
        {
            //Arr
            var createModel = new CriarUsuarioDto()
            {
                Email = "kakoferrare@gmail.com"
            };

            //Act
            var ex = Assert.ThrowsAsync<DomainException>(async () => await  _sut.CreateAsync(createModel));
            
            //Assert
            Assert.Equal("Alguns campos estão inválidos!", ex.Result.Message);
            Assert.NotNull(ex.Result.Errors);

        }


        [Fact]
        public void CreateAsync_EmptyStringEmail_ReturnDomainException()
        {
            //Arr
            var createModel = new CriarUsuarioDto()
            {
                Nome = "Kako"
            };

            //Act
            var ex = Assert.ThrowsAsync<DomainException>(async () => await _sut.CreateAsync(createModel));

            //Assert
            Assert.Equal("Alguns campos estão inválidos!", ex.Result.Message);
            Assert.NotNull(ex.Result.Errors);

        }

        [Fact]
        public void CreateAsync_InvalidStringEmail_ReturnDomainException()
        {
            //Arr
            var createModel = new CriarUsuarioDto()
            {
                Nome = "Kako",
                 Email = "kakoferraregmail.com"
            };

            //Act
            var ex = Assert.ThrowsAsync<DomainException>(async () => await _sut.CreateAsync(createModel));

            //Assert
            Assert.Equal("Alguns campos estão inválidos!", ex.Result.Message);
            Assert.NotNull(ex.Result.Errors);

        }

        [Fact]
        public async void CreateAsync_UserDtoAccepted_ReturnUserDto()
        {
            //Arr
            Usuario entity = new Usuario(1, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);
            var create = new CriarUsuarioDto()
            {
                Nome = "Marcos",
                Email ="kakoferrare@gmail.com"
            };

            _mockUsuarioRepository.Setup(c => c.CreateAsync(It.IsAny<Usuario>())).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.CreateAsync(create);

            //Assert
            _mockUsuarioRepository.Verify(c => c.CreateAsync(It.IsAny<Usuario>()), Times.Once);
            Assert.NotNull(result);

        }


        [Fact]
        public void UpdateAsync_EmptyStringNome_ReturnDomainException()
        {
            //Arr
            var updateModel = new AlterarUsuarioDto();

            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);


            _mockUsuarioRepository.Setup(c => c.ReadAsync(id)).ReturnsAsync(entity);
            var ex = Assert.ThrowsAsync<DomainException>(async () => await _sut.UpdateAsync(id, updateModel));

            //Assert
            Assert.Equal("Alguns campos estão inválidos!", ex.Result.Message);
            Assert.NotNull(ex.Result.Errors);

        }

        [Fact]
        public async void UpdateAsync_InvalidId_ReturnNull()
        {
            //Arr
            var updateModel = new AlterarUsuarioDto()
            {
                Nome = "Kako",
            };

            int id = It.IsAny<int>();
            Usuario entity = null;

            _mockUsuarioRepository.Setup(c => c.ReadAsync(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.UpdateAsync(id, updateModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.UpdateAsync(id, entity), Times.Never);
            Assert.Null(result);

        }


        [Fact]
        public async void UpdateAsync_UserDtoAccepted_ReturnUserDto()
        {
            //Arr
            var updateModel = new AlterarUsuarioDto()
            {
                Nome = "Kako",
            };

            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, DateTime.Now);

          
            _mockUsuarioRepository.Setup(c => c.ReadAsync(id)).ReturnsAsync(entity);
            _mockUsuarioRepository.Setup(c => c.UpdateAsync(id, entity)).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.UpdateAsync(id, updateModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.UpdateAsync(id, entity), Times.Once);
            Assert.NotNull(result);

        }


        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UsuarioDto, UsuarioModel>().ReverseMap();
                cfg.CreateMap<CriarUsuarioDto, UsuarioModel>().ReverseMap();
                cfg.CreateMap<AlterarUsuarioDto, UsuarioModel>().ReverseMap();
                cfg.CreateMap<UsuarioDto, Usuario>().ReverseMap();
                cfg.CreateMap<CriarUsuarioDto, Usuario>().ReverseMap();
                cfg.CreateMap<AlterarUsuarioDto, Usuario>().ReverseMap();
                cfg.CreateMap<Usuario, UsuarioModel>().ReverseMap();


            });
            _mapper = config.CreateMapper();
        }
    }
}
