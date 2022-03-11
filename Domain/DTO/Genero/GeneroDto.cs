namespace Domain.DTO.Usuario
{
    public class GeneroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
       
        /*public override bool Equals(object obj)
        {
            GeneroDto GeneroDto = (GeneroDto)obj;
            return Id.Equals(GeneroDto.Id) && Nome.Equals(GeneroDto.Nome);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }*/
    }
}
