using API.Controllers;
using API.Helpers;
using Domain.DTO.Usuario;
using Domain.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace API.UnitTest
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuarioService> serviceStub = new();
        private readonly UsuarioController _controller;
        public UsuarioControllerTest()
        {
            _controller = new UsuarioController(serviceStub.Object);
        }

        [Fact]
        public async Task Get_UsuarioNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            serviceStub.Setup(service => service.Get(It.IsAny<int>()))
                .ReturnsAsync((UsuarioDTO)null);


            // Act
            var result = await _controller.Get(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();

        }
        [Fact]
        public async Task Get_UsuarioExist_ReturnsExpectedUsuario()
        {
            // Arrange
            var expected = CreateUsuario();
            serviceStub.Setup(service => service.Get(It.IsAny<int>()))
                .ReturnsAsync(expected);


            // Act
            var result = await _controller.Get(expected.Id);

            // Assert
            var dto = (result as ActionResult<ApiSuccessResponse>).Value;
            dto.Data.Should().BeEquivalentTo(expected, options => options.ComparingByMembers<UsuarioDTO>());


        }
        [Fact]
        public async Task Get_UsuariosList_ReturnsExpectedUsuarios()
        {
            // Arrange
            var expectedList = new[] { CreateUsuario(), CreateUsuario(), CreateUsuario() };
            serviceStub.Setup(service => service.Get())
                .ReturnsAsync(expectedList);


            // Act
            var result = await _controller.Get();

            // Assert
            var dto = (result as ActionResult<ApiSuccessResponse>).Value;
            dto.Data.Should().BeEquivalentTo(expectedList, options => options.ComparingByMembers<IEnumerable<UsuarioDTO>>());
        }

        [Fact]
        public async Task CreateUsuario_UsuarioToCreate_ReturnsExpectedUsuarioCreated()
        {
            // Arrange
            var usuarioToCreate = new CriarUsuarioDTO() { Nome = "Kako", Email = "kakoferrare@gmail.com"};
           
            // Act
            var result = await _controller.Post(usuarioToCreate);

            // Assert
            var created = (result as ActionResult<ApiSuccessResponse>).Value;
            Assert.Equal(201, created.StatusCode);
            
        }
        [Fact]
        public async Task Update_UsuarioNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var existing = CreateUsuario();
            var usuarioId = existing.Id;
            var usuarioUpdate = new AlterarUsuarioDTO()
            {
                Nome = "Outro Nome"
            };
            serviceStub.Setup(service => service.Put(It.IsAny<int>(), usuarioUpdate))
               .ReturnsAsync((UsuarioDTO)null);

            // Act
            var result = await _controller.Put(usuarioId, usuarioUpdate);
            Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();

        }
        [Fact]
        public async Task Update_UsuarioExist_ReturnsExpectedUsuario()
        {
            // Arrange
            var existing = CreateUsuario();
            var usuarioId = existing.Id;
            var usuarioUpdate = new AlterarUsuarioDTO()
            {
                Nome = "Outro Nome"
            };
            serviceStub.Setup(service => service.Put(It.IsAny<int>(), usuarioUpdate))
               .ReturnsAsync(existing);

            // Act
            var result = await _controller.Put(usuarioId, usuarioUpdate);
            var update = (result as ActionResult<ApiSuccessResponse>).Value;
            // Assert
            Assert.Equal(200, update.StatusCode);

        }

        [Fact]
        public async Task Delete_UsuarioNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var existing = CreateUsuario();
            
            serviceStub.Setup(service => service.Delete(It.IsAny<int>()))
               .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(existing.Id);
            Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public async Task Delete_UsuarioExist_ReturnsExpectedUsuario()
        {
            // Arrange
            var existing = CreateUsuario();

            serviceStub.Setup(service => service.Delete(It.IsAny<int>()))
               .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(existing.Id);
            var delete = (result as ActionResult<ApiSuccessResponse>).Value;
            // Assert
            Assert.Equal(200, delete.StatusCode);

        }

        private UsuarioDTO CreateUsuario()
        {
            return new UsuarioDTO()
            {
                Id = 1,
                Nome = "Kako",
                Email = "kakoferrare87@gmail.com"
            };

        }


    }
}
