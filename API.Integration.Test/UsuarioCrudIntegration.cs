using API.Helpers;
using API.Integration.Test.Integration;
using Domain.DTO.Usuario;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test
{
    public class UsuarioCrudIntegration : BaseIntegration
    {
        public int _id{ get; set; }
        public string _nome { get; set; } = "Marcos";
        public string _email { get; set; } = "marcos@gmail.com";
        public int _genero { get; set; } = 1;

        [Fact]
        public async Task Should_Do_CrudUsuario()
        {
            var response = new HttpResponseMessage();
            var result = "";

            var UsuarioDto = new CriarUsuarioDto() { Nome = _nome, Email = _email, GeneroId = _genero };
            response = await PostJsonAsync(UsuarioDto, $"{hostApi}usuario", client);
            result = await response.Content.ReadAsStringAsync();

            var postResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDto>>(result);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_nome, postResponse.Data.Nome);
            Assert.Equal(_email, postResponse.Data.Email);
            Assert.True(postResponse.Data.Id != default(int));

            response = await GetAsync($"{hostApi}usuario", client);
            result = await response.Content.ReadAsStringAsync();
            var getListResponse = JsonConvert.DeserializeObject<DataSuccessResponse<List<UsuarioDto>>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(getListResponse.Data);


            _id = postResponse.Data.Id;
            response = await GetAsync($"{hostApi}usuario/{_id}", client);
            result = await response.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDto>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(getResponse.Data);


            var usuarioAlterarDto = new AlterarUsuarioDto() { Nome = _nome, GeneroId = _genero };
            usuarioAlterarDto.Nome = "KAKO";
            response = await PutJsonAsync(usuarioAlterarDto, $"{hostApi}usuario/{_id}", client);
            result = await response.Content.ReadAsStringAsync();

            var putResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDto>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(usuarioAlterarDto.Nome, putResponse.Data.Nome);
            Assert.Equal(_email, putResponse.Data.Email);
            Assert.True(putResponse.Data.Id != default(int));

           
            response = await DeleteAsync($"{hostApi}usuario/{_id}", client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Fact]
        public async Task Should_NotDo_CreateUsuario_NomeIsEmpty()
        {
            _ = new HttpResponseMessage();
            var UsuarioDto = new CriarUsuarioDto() { Nome = "", Email = _email, GeneroId = _genero };
            HttpResponseMessage response = await PostJsonAsync(UsuarioDto, $"{hostApi}usuario", client);
            var result = await response.Content.ReadAsStringAsync();

            var dataResult = JsonConvert.DeserializeObject<ErrorResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(dataResult.Message, string.Empty);
        }
        [Fact]
        public async Task Should_NotDo_CreateUsuario_EmailIsEmpty()
        {
            _ = new HttpResponseMessage();
            var UsuarioDto = new CriarUsuarioDto() { Nome = _nome, Email = "", GeneroId = _genero };
            HttpResponseMessage response = await PostJsonAsync(UsuarioDto, $"{hostApi}usuario", client);
            var result = await response.Content.ReadAsStringAsync();

            var dataResult = JsonConvert.DeserializeObject<ErrorResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(dataResult.Message, string.Empty);
        }
        [Fact]
        public async Task Should_NotDo_CreateUsuario_GeneroiIsEmpty()
        {
            _ = new HttpResponseMessage();
            var UsuarioDto = new CriarUsuarioDto() { Nome = _nome, Email = _email, GeneroId = 0};
            HttpResponseMessage response = await PostJsonAsync(UsuarioDto, $"{hostApi}usuario", client);
            var result = await response.Content.ReadAsStringAsync();

            var dataResult = JsonConvert.DeserializeObject<ErrorResponse>(result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(dataResult.Message, string.Empty);
        }
    }
}
