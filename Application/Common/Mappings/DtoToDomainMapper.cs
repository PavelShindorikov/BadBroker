using Application.Common.Models;
using Domain.Entities;

namespace Application.Common.Mappings;

public static class DtoToDomainMapper
{
    public static IEnumerable<BaseCurrency> ToBaseCurrencies(this IEnumerable<RateDto> rates, string baseName)
    {
        return rates.Select(x =>
            new BaseCurrency
            {
                Name = baseName,
                Date = x.Date,
                ExchangeRates = x.Rates!
                    .Select(r => new ExchangeRate
                    {
                        Name = r.Key,
                        Rate = r.Value
                    }).ToList()
            }).ToList();
    }
}
