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
        private readonly IUsuarioService _service;
        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiSuccessResponse>> Get()
        {
            try
            {
                var result = await _service.Get();
                return new ApiSuccessResponse((int)HttpStatusCode.OK, result, result.Count() > 0 ? "Usuários encontrados" : "Nenhum usuário encontrado");
            }
            catch (Exception e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message));
            }
        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult<ApiSuccessResponse>> Get(int id)
        {
            try
            {
                var result = await _service.Get(id);
                if(result == null)
                {
                    return NotFound();
                }
                return new ApiSuccessResponse((int)HttpStatusCode.OK, result, "Usuário encontrado");
            }

            catch (Exception e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiSuccessResponse>> Post([FromBody] CriarUsuarioDTO usuario)
        {
            try
            {
                var result = await _service.Post(usuario);
                return new ApiSuccessResponse((int)HttpStatusCode.Created, result, "Usuário cadastrado");
            }
            catch (DomainException e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message));
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ApiSuccessResponse>> Put(int id, [FromBody] AlterarUsuarioDTO usuario)
        {
            try
            {
                var result = await _service.Put(id, usuario);
                if (result != null)
                    return new ApiSuccessResponse((int)HttpStatusCode.OK, result, "Usuário alterado");
                return NotFound();
            }
            catch (DomainException e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ApiSuccessResponse>> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                if(!result)
                    return NotFound();
                return new ApiSuccessResponse((int)HttpStatusCode.OK, result, "Usuário excluído");
            }
            catch (Exception e)
            {
                return BadRequest(new ApiErrorResponse((int)HttpStatusCode.BadRequest, e.Message));
            }
        }
    }
}