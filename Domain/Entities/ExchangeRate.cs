namespace Domain.Entities;

public class ExchangeRate
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public decimal Rate { get; init; }
    public int BaseCurrencyId { get; init; }
    public BaseCurrency BaseCurrency { get; init; } = null!;
}