using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Get(int id);
        Task<IEnumerable<Usuario>> Get();
        Task<Usuario> Post(Usuario data);
        Task<Usuario> Put(int id, Usuario data);
        Task<bool> Delete(int id);
    }
}
