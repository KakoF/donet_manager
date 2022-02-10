using API.Helpers;
using API.Integration.Test.Integration;
using Domain.DTO.Usuario;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        public string _nome { get; set; }
        public string _email{ get; set; }

        [Fact]
        public async Task E_Possivel_Realizar_Crud_Usuario()
        {
            _nome = "Marcos";
            _email = "marcos@gmail.com";

            var usuarioDto = new CriarUsuarioDTO() { Nome = _nome, Email = _email };
            var response = await PostJsonAsync(usuarioDto, $"{hostApi}usuario", client);
            var postResult = await response.Content.ReadAsStringAsync();

            var postResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDTO>>(postResult);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(_nome, postResponse.Data.Nome);
            Assert.Equal(_email, postResponse.Data.Email);
            Assert.True(postResponse.Data.Id != default(int));
        }
    }
}
