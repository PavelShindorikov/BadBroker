namespace Application.Common.Models
{
    public class DateInterval
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}