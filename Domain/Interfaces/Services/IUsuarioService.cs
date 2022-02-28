using Domain.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ReadAsync(int id);
        Task<IEnumerable<UsuarioDTO>> ReadAsync();
        Task<UsuarioDTO> CreateAsync(CriarUsuarioDTO data);
        Task<UsuarioDTO> UpdateAsync(int id, AlterarUsuarioDTO data);
        Task<bool> DeleteAsync(int id);

    }
}
