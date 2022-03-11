using AutoMapper;
using Data.Implementations;
using Data.Interfaces.DataConnector;

using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Implementations;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroImplementation _implementation;
        private readonly IMapper _mapper;

        public GeneroService(IGeneroImplementation implementation, IMapper mapper)
        {
            _implementation = implementation;
            _mapper = mapper;

        }

        public async Task<IEnumerable<GeneroDto>> ReadAsync()
        {
            var list = await _implementation.ReadAsync();
            return _mapper.Map<IEnumerable<GeneroDto>>(list);
        }
    }
}
