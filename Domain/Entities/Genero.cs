using Newtonsoft.Json;
using System;
//Limpar os usings é sempre uma boa: Ctrl + R + G 

namespace Domain.Entities
{
    public class Genero : Base
    {
        [JsonProperty("nome")]
        public string Nome { get; private set; }

        public Genero() { }

        public Genero(int id, string nome, DateTime dataCriacao, DateTime? dataAlteracao) : base(id, dataCriacao, dataAlteracao)
        {
            Nome = nome;
        }
    }
}
