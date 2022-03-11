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
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _service;
        public GeneroController(IGeneroService service)
        {
            _service = service;
        }

        [HttpGet, Produces("application/json", Type = typeof(ListSuccessResponse<GeneroDto>))]
        public async Task<ActionResult<ListSuccessResponse<GeneroDto>>> Get()
        {
            var result = await _service.ReadAsync();
            return new ListSuccessResponse<GeneroDto>((int)HttpStatusCode.OK, result, result.Any() ? "Gêneros encontrados" : "Nenhum gênero encontrado");
        }

    }
}