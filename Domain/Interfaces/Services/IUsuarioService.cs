using Domain.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> Get(int id);
        Task<IEnumerable<UsuarioDTO>> Get();
        Task<UsuarioDTO> Post(CriarUsuarioDTO data);
        Task<UsuarioDTO> Put(int id, AlterarUsuarioDTO data);
        Task<bool> Delete(int id);

    }
}
