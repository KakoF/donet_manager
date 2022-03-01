using Domain.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> ReadAsync(int id);
        Task<IEnumerable<UsuarioDto>> ReadAsync();
        Task<UsuarioDto> CreateAsync(CriarUsuarioDto data);
        Task<UsuarioDto> UpdateAsync(int id, AlterarUsuarioDto data);
        Task<bool> DeleteAsync(int id);

    }
}
