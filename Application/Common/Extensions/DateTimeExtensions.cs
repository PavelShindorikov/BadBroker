using Application.Common.Models;

namespace Application.Common.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Return all dates between two dates
    /// </summary>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns></returns>
    public static IList<DateTime> EachDayTo(this DateTime dateFrom, DateTime dateTo)
    {
        return Enumerable.Range(0, dateTo.Subtract(dateFrom).Days + 1)
            .Select(i => dateFrom.AddDays(i))
            .ToList();
    }

    /// <summary>
    /// Enumerates all consecutive dates intervals in range of dates
    /// </summary>
    /// <param name="dates"></param>
    /// <returns></returns>
    public static IList<DateInterval> ToTimeIntervals(this IEnumerable<DateTime> dates)
    {
        var intervals = new List<DateInterval>();
        var start = dates.First();
        var end = dates.Last();
        var currentDay = start;
        foreach (var date in dates)
        {
            if (date > currentDay)
            {
                intervals.Add(new DateInterval(start, currentDay.AddDays(-1)));
                currentDay = start = date;
            }
            if (date == end) intervals.Add(new DateInterval(start, currentDay));
            currentDay = currentDay.AddDays(1);
        }
        return intervals;
    }
    //public static IEnumerable<DateInterval> ToTimeIntervals(this IEnumerable<DateTime> dates)
    //{
    //    var start = dates.First();
    //    var end = dates.Last();
    //    var currentDay = start;
    //    foreach (var date in dates)
    //    {
    //        if (date > currentDay)
    //        {
    //            yield return new DateInterval(start, currentDay.AddDays(-1));
    //            currentDay = start = date;
    //        }
    //        if (date == end) yield return new DateInterval(start, currentDay);
    //        currentDay = currentDay.AddDays(1);
    //    }
    //}
}

