using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IExternalApiService
{
    Task<TimeSeriesRates> GetRatesByTimeSeries(DateTime startDate, DateTime endDate, string baseCurrency,
        string exchangeCurrencies);
}
