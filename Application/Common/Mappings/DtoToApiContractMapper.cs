using Application.Common.Models;

namespace Application.Common.Mappings;

public static class DtoToApiContractMapper
{
    public static IList<Dictionary<string, dynamic>> ToRatesResponse(this IEnumerable<RateDto> rates)
    {
        return rates.Select(x =>
            new Dictionary<string, dynamic>
            {
                { nameof(x.Date).ToLower(), x.Date },

            }.Concat(x.Rates!.Select(r =>
                new KeyValuePair<string, dynamic>(r.Key.ToLower(), r.Value))
            ).ToDictionary(d => d.Key, d => d.Value))
            .ToList();
    }
}
