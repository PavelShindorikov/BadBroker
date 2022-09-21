using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    #region Bussines Entities
    DbSet<BaseCurrency> BaseCurrencies { get; }
    DbSet<ExchangeRate> ExchangeRates { get; }
    #endregion

    Task<int> SaveChangesAsync();
}
