using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces.Implementations
{
    //IGeneroImplementation não é um nome que diz muita coisa ao utilizá-lo numa injeção de dependência, por exemplo...
    // O Ideal é que seja chamada de IGeneroRepository
    public interface IGeneroImplementation : IRepository<Genero>
    {
       
    }
}
