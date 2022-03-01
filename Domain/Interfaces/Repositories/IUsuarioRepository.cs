using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ReadAsync(int id);
        Task<IEnumerable<Usuario>> ReadAsync();
        Task<Usuario> CreateAsync(Usuario data);
        Task<Usuario> UpdateAsync(int id, Usuario data);
        Task<bool> DeleteAsync(int id);
    }
}
