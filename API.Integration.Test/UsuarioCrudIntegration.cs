using API.Helpers;
using API.Integration.Test.Integration;
using Domain.DTO.Usuario;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test
{
    public class UsuarioCrudIntegration : BaseIntegration
    {
        public string _nome { get; set; }
        public string _email{ get; set; }

        [Fact]
        public async Task Should_Do_Crud_Usuario()
        {
            _nome = "Marcos";
            _email = "marcos@gmail.com";
            var response = new HttpResponseMessage();
            var result = "";

            var UsuarioDto = new CriarUsuarioDto() { Nome = _nome, Email = _email };
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


            var id = postResponse.Data.Id;
            response = await GetAsync($"{hostApi}usuario/{id}", client);
            result = await response.Content.ReadAsStringAsync();
            var getResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDto>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(getResponse.Data);


            var usuarioAlterarDto = new AlterarUsuarioDto() { Nome = _nome,};
            response = await PutJsonAsync(usuarioAlterarDto, $"{hostApi}usuario/{id}", client);
            result = await response.Content.ReadAsStringAsync();

            var putResponse = JsonConvert.DeserializeObject<DataSuccessResponse<UsuarioDto>>(result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(_nome, putResponse.Data.Nome);
            Assert.Equal(_email, putResponse.Data.Email);
            Assert.True(putResponse.Data.Id != default(int));

           
            response = await DeleteAsync($"{hostApi}usuario/{id}", client);
            result = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }
    }
}
