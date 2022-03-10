using Domain.Exceptions;
using Domain.Validators;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class UsuarioModel : BaseModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }

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

        public void SetCreate()
        {
            DataCriacao = DateTime.UtcNow;
        }

        public void SetUpdate()
        {
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}