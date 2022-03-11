using API.Helpers;
using Domain.DTO.Usuario;
using Domain.Exceptions;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //Evitar usar try catch nos controllers, as exceções devem ser tratadas se algo específico for necessário. Neste caso, vc pode usar um tratamento no startup da aplicação para que retorne badrequest em caso de erro não tratado;
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet, Produces("application/json", Type = typeof(ListSuccessResponse<UsuarioDto>))]
        public async Task<ActionResult<ListSuccessResponse<UsuarioDto>>> Get()
        {
            var result = await _service.ReadAsync();
            return new ListSuccessResponse<UsuarioDto>((int)HttpStatusCode.OK, result, result.Any() ? "Usuários encontrados" : "Nenhum usuário encontrado");
        }

        [HttpGet, Route("GetUsuarioGenero"), Produces("application/json", Type = typeof(ListSuccessResponse<UsuarioDto>))]
        public async Task<ActionResult<ListSuccessResponse<UsuarioDto>>> GetUsuarioGenero()
        {
            var result = await _service.ReadUsuarioGeneroAsync();
            return new ListSuccessResponse<UsuarioDto>((int)HttpStatusCode.OK, result, result.Any() ? "Usuários encontrados" : "Nenhum usuário encontrado");
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDto>>> Get(int id)
        {
            var result = await _service.ReadAsync(id);
            return new DataSuccessResponse<UsuarioDto>((int)HttpStatusCode.OK, result, result == null ? "Usuário não encontrado" : "Usuário encontrado");
        }

        [HttpPost]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDto>>> Post([FromBody] CriarUsuarioDto usuario)
        {
            var result = await _service.CreateAsync(usuario);
            Response.StatusCode = (int)HttpStatusCode.Created;
            return new DataSuccessResponse<UsuarioDto>((int)HttpStatusCode.Created, result, "Usuário cadastrado");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDto>>> Put(int id, [FromBody] AlterarUsuarioDto usuario)
        {
            var result = await _service.UpdateAsync(id, usuario);
            return new DataSuccessResponse<UsuarioDto>((int)HttpStatusCode.OK, result, "Usuário alterado");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return new Response((int)HttpStatusCode.OK, "Usuário excluído");
        }
    }
}