using API.Helpers;
using Domain.DTO.Clients.Advice;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdviceController : ControllerBase
    {
        private readonly IAdviceService _service;
        public AdviceController(IAdviceService service)
        {
            _service = service;
        }

        [HttpGet, Produces("application/json", Type = typeof(ListSuccessResponse<AdviceDto>))]
        public async Task<ActionResult<DataSuccessResponse<AdviceDto>>> Get()
        {
            var result = await _service.GetAsync();
            return new DataSuccessResponse<AdviceDto>((int)HttpStatusCode.OK, result, "Conselho encontrado");
        }
    }
}