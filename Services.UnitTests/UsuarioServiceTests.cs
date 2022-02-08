using Xunit;
using Moq;
using Domain.Interfaces.Repositories;
using Domain.Entities;
using Service.Services;
using AutoMapper;
using Domain.DTO.Usuario;

namespace Services.UnitTests
{
    public class UsuarioServiceTests
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IUsuarioRepository> _mockUsuarioRepoisitory;

        private readonly UsuarioService _usuarioService;
        IMapper _mapper;

        public UsuarioServiceTests()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);

            _mockUsuarioRepoisitory = _mockRepository.Create<IUsuarioRepository>();
        
            _usuarioService = new UsuarioService(_mockUsuarioRepoisitory.Object, _mapper, null, null);
        }

        [Fact]
        public async void When_SearchExistingUser_ShouldReturnPayload()
        {
            //Arr
            _mockUsuarioRepoisitory.Setup(c => c.Get(It.IsAny<int>()))
                .ReturnsAsync(new Usuario());

            //Act
            var result = await _usuarioService.Get(1);

            //Assert
            Assert.NotNull(result);

            //Testa se todos os repositório configurados no teste foram invovados, no exemplo só será invocado o repositório de usuário
            _mockRepository.VerifyAll();

            //Testa se o método Get foi invocado somente uma vez
            _mockUsuarioRepoisitory.Verify(c => c.Get(It.IsAny<int>()), Times.Once);
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
