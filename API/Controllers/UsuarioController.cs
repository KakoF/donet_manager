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

        [HttpGet, Produces("application/json", Type = typeof(ListSuccessResponse<UsuarioDTO>))]
        public async Task<ActionResult<ListSuccessResponse<UsuarioDTO>>> Get()
        {
            var result = await _service.Get();
            return new ListSuccessResponse<UsuarioDTO>((int)HttpStatusCode.OK, result, result.Count() > 0 ? "Usuários encontrados" : "Nenhum usuário encontrado");
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDTO>>> Get(int id)
        {
            var result = await _service.Get(id);
            return new DataSuccessResponse<UsuarioDTO>((int)HttpStatusCode.OK, result, "Usuário encontrado");
        }

        [HttpPost]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDTO>>> Post([FromBody] CriarUsuarioDTO usuario)
        {
            var result = await _service.Post(usuario);
            return new DataSuccessResponse<UsuarioDTO>((int)HttpStatusCode.Created, result, "Usuário cadastrado");
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<DataSuccessResponse<UsuarioDTO>>> Put(int id, [FromBody] AlterarUsuarioDTO usuario)
        {
            var result = await _service.Put(id, usuario);
            return new DataSuccessResponse<UsuarioDTO>((int)HttpStatusCode.OK, result, "Usuário alterado");
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var result = await _service.Delete(id);
            return new Response((int)HttpStatusCode.OK, "Usuário excluído");
        }
    }
}