using IntegratorHttpClient;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Integrator.UnitTests.Clients
{
    public class AdviceClientTests
    {
        private readonly AdviceClient _sut;
        private readonly MockRepository _mockRepository;
        private readonly Mock<HttpClient> _mockClient;

        public AdviceClientTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockClient = _mockRepository.Create<HttpClient>();
            _sut = new AdviceClient(_mockClient.Object);
        }

        [Fact]
        public async void GetAsync_ReturnAdviceToClient()
        {
            // Arrange
            /*string stringResponse = JsonConvert.SerializeObject(new AdviceDto()
            {
                Slip = new SlipDto() { Advice = "asdasd", Id = 1 }
            });*/
            _mockClient.Object.BaseAddress = new Uri("https://api.adviceslip.com/");

            // Act
            var result = await _sut.Get("advice");

            // Assert
            Assert.NotNull(result);
        }
    }
}
