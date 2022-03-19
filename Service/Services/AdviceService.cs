using AutoMapper;
using Data.Implementations;
using Data.Interfaces.DataConnector;
using Domain.DTO.Clients.Advice;
using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Implementations;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using IntegratorHttpClient.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AdviceService : IAdviceService
    {
        private readonly IAdviceClient _httpClient;
        private readonly IMapper _mapper;

        public AdviceService(IAdviceClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;

        }

        public async Task<AdviceDto> GetAsync()
        {
            var advice = await _httpClient.Get("advice");
            return _mapper.Map<AdviceDto>(advice);
        }
    }
}
