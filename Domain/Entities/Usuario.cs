using Newtonsoft.Json;
using System;
//Limpar os usings é sempre uma boa: Ctrl + R + G 

namespace Domain.Entities
{
    public class Usuario : Base
    {
        [JsonProperty("nome")]
        public string Nome { get; private set; }
        [JsonProperty("email")]
        public string Email { get; private set; }
        public int GeneroId { get; private set; }
        public Genero Genero { get; private set; }
        public Usuario() { }

        public Usuario(int id, string nome, string email, int generoId, DateTime dataCriacao, DateTime? dataAlteracao) : base(id, dataCriacao, dataAlteracao)
        {
            Nome = nome;
            Email = email;
            GeneroId = generoId;
        }

        public void InitGenero(Genero genero)
        {
           Genero = genero;
        }

        public void SetGeneroId(int generoId)
        {
            GeneroId = generoId;
        }
    }
}
