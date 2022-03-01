using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Usuario
{
    public class AlterarUsuarioDto
    {
        [Required(ErrorMessage = "Nome é campo obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
    }
}
