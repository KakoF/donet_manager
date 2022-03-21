
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