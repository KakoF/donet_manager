using Domain.Entities;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Implementations
{
    public interface IGeneroImplementation : IRepository<Genero>
    {
        public Task<IEnumerable<Genero>> QueryGraphql();
        public Task<Genero> QueryGraphql(int id);
    }
}
