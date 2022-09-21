namespace Application.Common.Models
{
    public class RateDto
    {
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; init; } = new ();
    }
}