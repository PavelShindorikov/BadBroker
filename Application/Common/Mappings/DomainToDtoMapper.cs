using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Mappings;

public static class DomainToDtoMapper
{
    public static IEnumerable<RateDto> ToRatesDto(this IEnumerable<BaseCurrency> rates)
    {
        return rates.Select(x => new RateDto
        {
            Date = x.Date,
            Rates = x.ExchangeRates
                .ToDictionary(r => r.Name, r => r.Rate)
        });
    }
}