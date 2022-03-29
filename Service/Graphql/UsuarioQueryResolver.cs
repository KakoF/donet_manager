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
    public class UsuarioQueryResolver
    {
        public async Task<IEnumerable<UsuarioDto>> Usuarios([Service] IUsarioImplementation _implementation, [Service] IMapper _mapper)
        {
            var usuarios =  await _implementation.QueryGraphql();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }

        public async Task<UsuarioDto> Usuario([Service] IUsarioImplementation _implementation, [Service] IMapper _mapper, int id)
        {
            var usuario = await _implementation.QueryGraphql(id);
            return _mapper.Map<UsuarioDto>(usuario);
        }
    }
}
