namespace Domain.DTO.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int GeneroId { get; set; }
        public GeneroDto Genero { get; set; }
       
    }
}
