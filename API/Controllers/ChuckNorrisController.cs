using API.Helpers;
using Domain.DTO.Clients.Advice;
using Domain.DTO.Clients.ChuckNorris;
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
    public class ChuckNorrisController : ControllerBase
    {
        private readonly IChuckNorrisService _service;
        public ChuckNorrisController(IChuckNorrisService service)
        {
            _service = service;
        }

        [HttpGet, Produces("application/json", Type = typeof(ListSuccessResponse<AdviceDto>))]
        public async Task<ActionResult<DataSuccessResponse<ChuckNorrisDto>>> Get()
        {
            var result = await _service.GetAsync();
            return new DataSuccessResponse<ChuckNorrisDto>((int)HttpStatusCode.OK, result, "Paranauê encontrado");
        }
    }
}