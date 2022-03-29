using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//Limpar os usings é sempre uma boa: Ctrl + R + G 

namespace Domain.Entities
{
    public class Genero : Base
    {
        [JsonProperty("nome")]
        public string Nome { get; private set; }

        public List<Usuario> Usuarios { get;  private set; }

        public Genero() { }

        public Genero(int id, string nome, DateTime dataCriacao, DateTime? dataAlteracao) : base(id, dataCriacao, dataAlteracao)
        {
            Nome = nome;
        }

        public void UsuariosListInit()
        {
            Usuarios = new List<Usuario>();
        }
    }
}
