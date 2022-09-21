namespace Application.Common.Models
{
    public class TimeSeriesRates
    {
        public Dictionary<DateTime, Dictionary<string, decimal>> Rates { get; set; } = new ();
    }
}