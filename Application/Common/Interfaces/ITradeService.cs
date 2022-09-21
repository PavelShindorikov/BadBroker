using Application.Contracts;

namespace Application.Common.Interfaces;

public interface ITradeService
{
    Task<BestRevenueResponse> GetBestRevenueAsync(BestRevenueRequest bestRevenue);
}