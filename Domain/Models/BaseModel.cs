using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public abstract class BaseModel
    {
        public DateTime DataCriacao { get; set; }
        
        public DateTime? DataAtualizacao { get; set; }

        internal List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;
        public abstract bool Validate();

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
