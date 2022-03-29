using AutoMapper;
using Domain.DTO.Usuario;
using Domain.Entities;
using Domain.Interfaces.Implementations;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Graphql
{
    [ExtendObjectType("Query")]
    public class GeneroQueryResolver
    {
        public async Task<IEnumerable<GeneroDto>> Generos([Service] IGeneroImplementation _implementation, [Service] IMapper _mapper)
        {
            var generos =  await _implementation.QueryGraphql();
            return _mapper.Map<IEnumerable<GeneroDto>>(generos);
        }

        public async Task<GeneroDto> Genero([Service] IGeneroImplementation _implementation, [Service] IMapper _mapper, int id)
        {
            var genero = await _implementation.QueryGraphql(id);
            return _mapper.Map<GeneroDto>(genero);
        }
    }
}
