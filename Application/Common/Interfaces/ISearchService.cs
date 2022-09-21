using Application.Common.Models;
using Application.Contracts;

namespace Application.Common.Interfaces;

public interface ISearchService
{
    BestRevenueResponse SearchBestRevenue(IList<RateDto> rates, decimal amount, int brokerFee);
}