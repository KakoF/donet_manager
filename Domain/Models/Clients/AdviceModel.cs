using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Clients
{
    public class AdviceModel
    {
        [JsonProperty("slip")]
        public SlipModel Slip { get; set; }
    }

    public class SlipModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("advice")]
        public string Advice { get; set; }
    }
}