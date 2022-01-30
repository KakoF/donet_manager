using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class BaseModel
    {
        public DateTime DataCriacao { get; set; }
        
        public DateTime? DataAtualizacao { get; set; }

        internal List<string> _errors;
        public IReadOnlyCollection<string> Errors => _errors;
        public abstract bool Validate();
    }
}
