using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Usuario
{
    public class CriarUsuarioDto
    {
        [Required(ErrorMessage = "Nome é campo obrigatório.")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "E-mail é campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [StringLength(180, ErrorMessage = "E-mail deve ter no máximo {1} caracteres.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Gênero é campo obrigatório.")]
        public int GeneroId { get; set; }
    }
}
