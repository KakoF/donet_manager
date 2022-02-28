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

namespace Services.UnitTests
{
    public class UsuarioServiceTests
    {
        private readonly UsuarioService _sut;


        private readonly MockRepository _mockRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepoisitory;
        Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        Mock<IRedisIntegrator> _mockRedisIntegrator = new Mock<IRedisIntegrator>();
        
        IMapper _mapper;

        public UsuarioServiceTests()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockUsuarioRepoisitory = _mockRepository.Create<IUsuarioRepository>();
            _sut = new UsuarioService(_mockUsuarioRepoisitory.Object, _mapper, _mockUnitOfWork.Object, _mockRedisIntegrator.Object);

        }

        [Fact]
        public async void When_GetPassingId_ShouldReturnUser()
        {
            //Arr
            int id = It.IsAny<int>();
            Usuario entity = new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null);
            _mockUsuarioRepoisitory.Setup(c => c.Get(id)).ReturnsAsync(entity);

            //Act
            var result = await _sut.Get(id);

            //Assert
            _mockUsuarioRepoisitory.Verify(c => c.Get(id), Times.Once);

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
            _mockUsuarioRepoisitory.Setup(c => c.Get()).ReturnsAsync(entitys);

            //Act
            var result = await _sut.Get();

            //Assert
            _mockUsuarioRepoisitory.Verify(c => c.Get(), Times.Once);
            Assert.Equal(result.Count(), _mapper.Map<List<UsuarioDTO>>(entitys).Count);
        }


        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            });

            _mapper = config.CreateMapper();
        }
    }
}
