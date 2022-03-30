using IntegratorHttpClient;
using Moq;
using System;
using System.Net.Http;
using Xunit;

namespace Integrator.UnitTests.Clients
{
    public class ChukNorrisClientTests
    {
        private readonly ChukNorrisClient _sut;
        private readonly MockRepository _mockRepository;
        private readonly Mock<HttpClient> _mockClient;


        public ChukNorrisClientTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockClient = _mockRepository.Create<HttpClient>();
            _sut = new ChukNorrisClient(_mockClient.Object);
        }

        [Fact]
        public async void GetAsync_ReturnChuckNorrisToClient()
        {
            // Arrange
            _mockClient.Object.BaseAddress = new Uri("https://api.chucknorris.io/");

            // Act
            var result = await _sut.Get("jokes/random");

            // Assert
            Assert.NotNull(result);
        }
    }
}
