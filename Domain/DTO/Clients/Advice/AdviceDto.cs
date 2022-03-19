using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Clients.Advice
{
    public class AdviceDto
    {
        public SlipDto Slip { get; set; }
    }

    public class SlipDto
    {
        public int Id { get; set; }
        public string Advice { get; set; }
    }
}