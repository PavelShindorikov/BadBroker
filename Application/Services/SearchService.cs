using Application.Common.Interfaces;
using Application.Contracts;
using Application.Common.Models;
using Application.Common.Mappings;

namespace Application.Services;

/// <summary>
/// Search service
/// </summary>
public class SearchService : ISearchService
{
    /// <summary>
    /// Search best revenue for specified period of rates and trader money
    /// </summary>
    /// <param name="rates"></param>
    /// <param name="amountMoney"></param>
    /// <param name="brokerFee"></param>
    /// <returns></returns>
    public BestRevenueResponse SearchBestRevenue(IList<RateDto> rates, decimal amountMoney, int brokerFee)
    {
        if (rates == null) 
            throw new ArgumentNullException($"Argument {nameof(rates)} is null. Method {nameof(SearchBestRevenue)}.");

        var result = new BestRevenueResponse();
        var bestRevenue = 0m;
        var currencies = rates
            .SelectMany(r => r.Rates)
            .GroupBy(d => d.Key)
            .Select(k => k.Key);

        foreach (var currency in currencies)
        {
            var rateCount = rates.Count;
            for (var i = 0; i < rateCount - 1; i++)
            {
                //skip iteration when graph grows
                if (rates[i].Rates[currency] <= rates[i + 1].Rates[currency]) continue;
                var buyPrice = rates[i].Rates[currency];

                for (var j = i + 1; j < rateCount; j++)
                {
                    var sellPrice = rates[j].Rates[currency];
                    //skip iteration when graph grows
                    if (rates[j - 1].Rates![currency!] <= sellPrice || sellPrice == 0) continue;
                    //calculate revenue
                    var revenue = (buyPrice * amountMoney / sellPrice) -
                                  (brokerFee * (rates[j].Date - rates[i].Date).Days) - amountMoney;
                    //skip iteration when not positive condition
                    if (revenue <= bestRevenue || revenue < 0) continue;
                    bestRevenue = revenue;
                    result = new BestRevenueResponse
                    {
                        BuyDate = rates[i].Date,
                        SellDate = rates[j].Date,
                        Tool = currency,
                        Revenue = bestRevenue,
                    };
                }
            }
        }
        return result;
    }
    
}