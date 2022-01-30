using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UsuarioModel
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        private DateTime _dataCriacao;
        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set { _dataCriacao = (value == null ? DateTime.UtcNow : value); }
        }

        private DateTime _dataAtualizacao;
        public DateTime DataAtualizacao
        {
            get { return _dataAtualizacao; }
            set { _dataAtualizacao = (DataCriacao != null ? DateTime.UtcNow : value); }
        }
    }
}