using Xunit;
using Moq;
using Service.Services;
using AutoMapper;
using IntegratorHttpClient.interfaces;
using Domain.DTO.Clients.Advice;
using Domain.Models.Clients;

namespace Services.UnitTests
{
    public class AdviceServiceTest
    {
        private readonly AdviceService _sut;


        private readonly MockRepository _mockRepository;
        private readonly Mock<IAdviceClient> _mockAdviceClient;
        IMapper _mapper;

        public AdviceServiceTest()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockAdviceClient = _mockRepository.Create<IAdviceClient>();
            _sut = new AdviceService(_mockAdviceClient.Object, _mapper);

        }


        [Fact]
        public async void GetAsync_ReturnListGenero()
        {
            //Arr
            var advice = new AdviceModel()
            {
                Slip = new SlipModel()
                {
                    Id = 1,
                    Advice = "Advice",
                }
            };
            _mockAdviceClient.Setup(c => c.Get("advice")).ReturnsAsync(advice);

            //Act
            var result = await _sut.GetAsync();

            //Assert
            _mockAdviceClient.Verify(c => c.Get("advice"), Times.Once);
            Assert.NotNull(result);
        }

        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AdviceDto, AdviceModel>().ReverseMap();
                cfg.CreateMap<SlipDto, SlipModel>().ReverseMap();

            });
            _mapper = config.CreateMapper();
        }
    }
}
