using System.Collections.Generic;

namespace Domain.DTO.Usuario
{
    public class GeneroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<UsuarioDto> Usuarios { get; set; }
    }
}
