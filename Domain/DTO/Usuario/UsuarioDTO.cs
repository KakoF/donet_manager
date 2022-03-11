namespace Domain.DTO.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int GeneroId { get; set; }
        public GeneroDto Genero { get; set; }

        /*public override bool Equals(object obj)
        {
            UsuarioDto UsuarioDto = (UsuarioDto)obj;
            return Id.Equals(UsuarioDto.Id) && Nome.Equals(UsuarioDto.Nome) && Email.Equals(UsuarioDto.Email);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }*/
    }
}
