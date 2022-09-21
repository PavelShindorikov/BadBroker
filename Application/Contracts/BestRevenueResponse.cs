using Application.Common.Models;

namespace Application.Contracts;
public class BestRevenueResponse
{
    public IList<Dictionary<string, dynamic>> Rates { get; set; } = new List<Dictionary<string, dynamic>>();
    public DateTime? BuyDate { get; init; }
    public DateTime? SellDate { get; init; }
    public string? Tool { get; init; }
    public decimal Revenue { get; init; } = default!;

}
