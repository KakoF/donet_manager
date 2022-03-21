using Xunit;
using Moq;
using Service.Services;
using AutoMapper;
using IntegratorHttpClient.interfaces;
using Domain.Models.Clients;
using Domain.DTO.Clients.ChuckNorris;
using System;

namespace Services.UnitTests
{
    public class ChuckNorrisServiceTest
    {
        private readonly ChuckNorrisService _sut;


        private readonly MockRepository _mockRepository;
        private readonly Mock<IChukNorrisClient> _mockChuckNorrisClient;
        IMapper _mapper;

        public ChuckNorrisServiceTest()
        {
            InitializedMapper();

            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockChuckNorrisClient = _mockRepository.Create<IChukNorrisClient>();
            _sut = new ChuckNorrisService(_mockChuckNorrisClient.Object, _mapper);

        }


        [Fact]
        public async void GetAsync_ReturnListGenero()
        {
            //Arr
            var chuckNorrisDo = new ChuckNorrisModel()
            {
                id = "1",
                created_at = DateTime.Now.ToShortDateString(),
                icon_url = "url"
            };
            _mockChuckNorrisClient.Setup(c => c.Get("jokes/random")).ReturnsAsync(chuckNorrisDo);

            //Act
            var result = await _sut.GetAsync();

            //Assert
            _mockChuckNorrisClient.Verify(c => c.Get("jokes/random"), Times.Once);
            Assert.NotNull(result);
        }

        private void InitializedMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ChuckNorrisDto, ChuckNorrisModel>().ReverseMap();

            });
            _mapper = config.CreateMapper();
        }
    }
}
