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
        public async Task<IEnumerable<Usuario>> Get([Service] IUsarioImplementation implementation)
        {
            return await implementation.QueryGraphql();
        }

        public async Task<Usuario> GetOne([Service] IUsarioImplementation implementation, int id)
        {
            return await implementation.QueryGraphql(id);
        }
    }
}
