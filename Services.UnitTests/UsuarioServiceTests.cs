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
        public async void When_GetPassingId_ShouldReturnUser()
        {
            //Arr
            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);
            _mockUsuarioRepository.Setup(c => c.Get(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.Get(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.Get(id), Times.Once);

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
            _mockUsuarioRepository.Setup(c => c.Get()).ReturnsAsync(entitys);

            //Act
            var result = await _sut.Get();

            //Assert
            _mockUsuarioRepository.Verify(c => c.Get(), Times.Once);
            Assert.Equal(result.Count(), _mapper.Map<List<UsuarioDTO>>(entitys).Count);
        }

        [Fact]
        public async void When_Delete_ShouldDeleteUserWhenIdValidAndReturnTrue()
        {
            //Arr
            int id = It.IsAny<int>();
            _mockUsuarioRepository.Setup(c => c.Delete(id)).ReturnsAsync(true);

            //Act
            var result = await _sut.Delete(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.Delete(id), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async void When_Delete_ShouldNotDeleteUserWhenIdInvalidAndReturnFalse()
        {
            //Arr
            int id = It.IsAny<int>();
            _mockUsuarioRepository.Setup(c => c.Delete(id)).ReturnsAsync(false);

            //Act
            var result = await _sut.Delete(id);

            //Assert
            _mockUsuarioRepository.Verify(c => c.Delete(id), Times.Once);
            Assert.False(result);
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

            var model = _mapper.Map<UsuarioModel>(new UsuarioModel()
            {
                Nome = createModel.Nome,
                Email = createModel.Email,
                DataCriacao = DateTime.Now,
                DataAtualizacao = null
            });

            Usuario entity = new Usuario(It.IsAny<int>(), model.Nome, model.Email, model.DataCriacao, model.DataAtualizacao);

            _mockUsuarioRepository.Setup(c => c.Post(entity)).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.Post(createModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.Post(entity), Times.Once);
            Assert.NotNull(result);

        }



        [Fact]
        public async void When_Put_ShouldUpdateUserAndReturn()
        {
            
            var updateModel = new AlterarUsuarioDTO()
            {
                Nome = "Kako",
            };

            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);

            var model = _mapper.Map<UsuarioModel>(new UsuarioModel() { 
                Nome = updateModel.Nome,
                Email = entity.Email,
                DataCriacao = entity.DataCriacao,
                DataAtualizacao = DateTime.Now
            });
            var modelEntity = _mapper.Map<Usuario>(model);

            _mockUsuarioRepository.Setup(c => c.Get(id)).ReturnsAsync(entity);
            _mockUsuarioRepository.Setup(c => c.Put(id, modelEntity)).ReturnsAsync(entity);
            _mockUnitOfWork.Setup(c => c.CommitTransaction());

            //Act
            var result = await _sut.Put(id, updateModel);

            //Assert
            _mockUsuarioRepository.Verify(c => c.Put(id, entity), Times.Once);
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
