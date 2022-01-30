using Domain.Exceptions;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UsuarioModel : BaseModel
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

        public UsuarioModel()
        {
            _errors = new List<string>();
        }

        public override bool Validate()
        {
            var validator = new UsuarioValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                    _errors.Add(error.ErrorMessage);

                throw new DomainException($"Alguns campos estão inválidos!", _errors);
            }

            return true;
        }
    }
}