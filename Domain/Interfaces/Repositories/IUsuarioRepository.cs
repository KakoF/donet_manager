using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetAsync(int id);
        Task<IEnumerable<Usuario>> GetAsync();
        Task<Usuario> PostAsync(Usuario data);
        Task<Usuario> PutAsync(int id, Usuario data);
        Task<bool> DeleteAsync(int id);
    }
}
