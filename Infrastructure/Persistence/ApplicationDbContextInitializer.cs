using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitializer> _logger;

    public ApplicationDbContextInitializer(
        ApplicationDbContext context,
        ILogger<ApplicationDbContextInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsSqlite())
            {
                await _context.Database.EnsureCreatedAsync();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while initializing the database.");
            throw;
        }

    }
}
