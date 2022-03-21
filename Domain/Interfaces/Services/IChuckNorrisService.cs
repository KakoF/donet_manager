using Domain.DTO.Clients.Advice;
using Domain.DTO.Clients.ChuckNorris;
using Domain.DTO.Usuario;
using Domain.Models.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IChuckNorrisService
    {
        Task<ChuckNorrisDto> GetAsync();
    }
}
