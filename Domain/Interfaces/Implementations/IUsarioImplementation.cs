using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Implementations
{
    public interface IUsarioImplementation : IRepository<Usuario>
    {
        public Task<IEnumerable<Usuario>> ReadUsuarioGeneroAsync();
        public Task<IEnumerable<Usuario>> QueryGraphql();
        public Task<Usuario> QueryGraphql(int id);
    }
}
