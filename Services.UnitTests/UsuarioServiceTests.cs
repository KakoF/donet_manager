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
        public async void When_GetPassingId_ShouldReturnCachedUser()
        {
            //Arr
            int id = It.IsAny<int>();
            UsuarioDTO usuarioDto = new UsuarioDTO() 
            { 
                Id = id, 
                Nome = "Marcos", 
                Email= "kakoferrare@gmail.com" 
            };
            _mockRedisIntegrator.Setup(c => c.GetAsync<UsuarioDTO>($"Usuario_{id}")).ReturnsAsync(usuarioDto);

            //Act
            var result = await _sut.ReadAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.GetAsync(id), Times.Never);

            Assert.Equal(result.Id, id);
            Assert.True(result.Equals(_mapper.Map<UsuarioDTO>(usuarioDto)));

        }

        [Fact]
        public async void When_GetPassingId_ShouldReturnUser()
        {
            //Arr
            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);
            _mockUsuarioRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.ReadAsync(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.GetAsync(id), Times.Once);

            Assert.Equal(result.Id, id);
            Assert.True(result.Equals(_mapper.Map<UsuarioDTO>(entity)));

        }


        [Fact]
        public async void When_Get_ShouldReturnUserList()
        {
            //Arr
            var entitys = new List<Usuario>()
            {
                new Usuario(1, "Marcos", "marcosferrare@gmail.com", DateTime.Now, null),
                new Usuario(2, "Kako", "kakoferrare@gmail.com", DateTime.Now, null),
            };
            _mockUsuarioRepository.Setup(c => c.GetAsync()).ReturnsAsync(entitys);

            //Act
            var result = await _sut.ReadAsync();

            //Assert
            _mockUsuarioRepository.Verify(c => c.GetAsync(), Times.Once);
            Assert.Equal(result.Count(), _mapper.Map<List<UsuarioDTO>>(entitys).Count);
        }

        [Fact]
        public async void When_Delete_ShouldDeleteUserWhenIdValidAndReturnTrue()
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
        public async void When_Delete_ShouldNotDeleteUserWhenIdInvalidAndReturnFalse()
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
        public void When_PostWhitoutName_ShouldReturnError()
        {
            //Arr
            var createModel = new CriarUsuarioDTO()
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
        public void When_PostWhitoutEmail_ShouldReturnError()
        {
            //Arr
            var createModel = new CriarUsuarioDTO()
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
        public void When_PostWhitInvalidEmail_ShouldReturnError()
        {
            //Arr
            var createModel = new CriarUsuarioDTO()
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
        public async void When_Post_ShouldCreateUserAndReturn()
        {
            //Arr
            var createModel = new CriarUsuarioDTO()
            {
                Nome = "Kako",
                Email = "kakoferrare@gmail.com"
            };

            Usuario entity = new Usuario(1, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);

            _mockUsuarioRepository.Setup(c => c.PostAsync(entity)).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.CreateAsync(createModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.PostAsync(entity), Times.Once);
            Assert.NotNull(result);

        }


        [Fact]
        public void When_PutWhitoutName_ShouldReturnError()
        {
            //Arr
            var updateModel = new AlterarUsuarioDTO();

            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);


            _mockUsuarioRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(entity);
            var ex = Assert.ThrowsAsync<DomainException>(async () => await _sut.UpdateAsync(id, updateModel));

            //Assert
            Assert.Equal("Alguns campos estão inválidos!", ex.Result.Message);
            Assert.NotNull(ex.Result.Errors);

        }

        [Fact]
        public async void When_PutIdNotFound_ShouldReturnNull()
        {
            //Arr
            var updateModel = new AlterarUsuarioDTO()
            {
                Nome = "Kako",
            };

            int id = It.IsAny<int>();
            Usuario entity = null;

            _mockUsuarioRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.UpdateAsync(id, updateModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.PutAsync(id, entity), Times.Never);
            Assert.Null(result);

        }


        [Fact]
        public async void When_Put_ShouldUpdateUserAndReturn()
        {
            //Arr
            var updateModel = new AlterarUsuarioDTO()
            {
                Nome = "Kako",
            };

            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, DateTime.Now);

          
            _mockUsuarioRepository.Setup(c => c.GetAsync(id)).ReturnsAsync(entity);
            _mockUsuarioRepository.Setup(c => c.PutAsync(id, entity)).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.UpdateAsync(id, updateModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.PutAsync(id, entity), Times.Once);
            Assert.NotNull(result);

        }


        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UsuarioDTO, Usuario>().ReverseMap();
                cfg.CreateMap<CriarUsuarioDTO, Usuario>().ReverseMap();
                cfg.CreateMap<AlterarUsuarioDTO, Usuario>().ReverseMap();
                cfg.CreateMap<UsuarioModel, AlterarUsuarioDTO>().ReverseMap();
                cfg.CreateMap<UsuarioModel, CriarUsuarioDTO>().ReverseMap();
                cfg.CreateMap<UsuarioModel, Usuario>().ReverseMap();
                

            });

            _mapper = config.CreateMapper();
        }
    }
}
