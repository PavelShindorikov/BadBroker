using Domain.Entities;

namespace Application.Common.Interfaces;

public interface IRateRepository
{
    Task<IEnumerable<BaseCurrency>> GetRatesAsync(string currency, DateTime starDate, DateTime enDate);
    Task AddRatesAsync(IEnumerable<BaseCurrency> entities);
}