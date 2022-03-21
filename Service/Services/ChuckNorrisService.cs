using AutoMapper;
using Data.Implementations;
using Data.Interfaces.DataConnector;
using Domain.DTO.Clients.Advice;
using Domain.DTO.Clients.ChuckNorris;
using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Implementations;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.Clients;
using IntegratorHttpClient.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ChuckNorrisService : IChuckNorrisService
    {
        private readonly IChukNorrisClient _httpClient;
        private readonly IMapper _mapper;

        public ChuckNorrisService(IChukNorrisClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;

        }


        public async Task<ChuckNorrisDto> GetAsync()
        {
            var chuckDo = await _httpClient.Get("jokes/random");
            return _mapper.Map<ChuckNorrisDto>(chuckDo);
        }
    }
}
