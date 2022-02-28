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
        public async void When_SearchExistingUser_ShouldReturnPayload()
        {
            //Arr
            int id = It.IsAny<int>();
            _mockUsuarioRepoisitory.Setup(c => c.Get(id)).ReturnsAsync(new Usuario(id, "Marcos", "kakoferrare@gmail.com", DateTime.Now, null));

            //Act
            var result = await _sut.Get(id);

            //Assert
            _mockUsuarioRepoisitory.Verify(c => c.Get(id), Times.Once);
            Assert.Equal(result.Id, id);
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
