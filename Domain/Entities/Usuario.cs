using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Limpar os usings é sempre uma boa: Ctrl + R + G 

namespace Domain.Entities
{
    public class Usuario : Base
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
