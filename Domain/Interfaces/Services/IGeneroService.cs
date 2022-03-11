using Domain.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ReadAsync();
    }
}
