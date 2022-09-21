using Application.Common.Models;

namespace Application.Common.Mappings;

public static class ExternalApiToDtoMapper
{
    public static IList<RateDto> ToRatesDto(this TimeSeriesRates tsRates)
    {
        return tsRates.Rates.Select(x =>
            new RateDto
            {
                Date = x.Key,
                Rates = x.Value
            }).ToList();
    }
}