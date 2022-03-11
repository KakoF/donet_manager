using Domain.Exceptions;
using Domain.Validators;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class GeneroModel : BaseModel
    {
        public string Nome { get; set; }

        public GeneroModel()
        {
            _errors = new List<string>();
        }

        public override bool Validate()
        {
            var validator = new GeneroValidator();
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