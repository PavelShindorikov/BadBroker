namespace Domain.Entities;

public class BaseCurrency
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public DateTime Date { get; init; }
    public IList<ExchangeRate> ExchangeRates { get; init; } = new List<ExchangeRate>();
}