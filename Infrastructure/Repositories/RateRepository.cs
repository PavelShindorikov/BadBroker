using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

/// <summary>
/// Rates repository
/// </summary>
public class RateRepository : IRateRepository
{
    private readonly IApplicationDbContext _сontext;
    private readonly ILogger<RateRepository> _logger;

    public RateRepository(
        IApplicationDbContext сontext,
        ILogger<RateRepository> logger)
    {
        _сontext = сontext;
        _logger = logger;
    }

    /// <summary>
    /// Get list of rates
    /// </summary>
    /// <param name="currency"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BaseCurrency>> GetRatesAsync(string currency, DateTime startDate, DateTime endDate)
    {
        try
        {
            return await _сontext.BaseCurrencies
                .AsNoTracking()
                .Include(x => x.ExchangeRates)
                .Where(x => x.Name == currency)
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "DataBaseException");
            throw;
        }
    }

    /// <summary>
    /// Add range of rates
    /// </summary>
    /// <param name="entities"></param>
    public async Task AddRatesAsync(IEnumerable<BaseCurrency> entities)
    {
        try
        {
            _сontext.BaseCurrencies.AddRange(entities);
            await _сontext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "DataBaseException");
            throw;
        }
    }
}

