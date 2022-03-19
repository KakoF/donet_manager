using Domain.DTO.Clients.Advice;
using Domain.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAdviceService
    {
        Task<AdviceDto> GetAsync();
    }
}
