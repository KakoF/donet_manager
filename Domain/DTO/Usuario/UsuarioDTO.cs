namespace Domain.DTO.Usuario
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            UsuarioDTO usuarioDto = (UsuarioDTO)obj;
            return Id.Equals(usuarioDto.Id) && Nome.Equals(usuarioDto.Nome) && Email.Equals(usuarioDto.Email);
        }
    }
}
