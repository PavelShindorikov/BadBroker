using Application.Common.Interfaces;
using Application.Common.Settings;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddTransient<IRateRepository, RateRepository>();

        services.AddOptions();

        services.Configure<CurrentAppSettings>(configuration.GetSection(nameof(CurrentAppSettings)));

        var fixerApi = configuration
            .GetSection(nameof(CurrentAppSettings))
            .GetSection(nameof(FixerApiSettings));
        
        services.AddHttpClient<IExternalApiService, FixerApiService>(client =>
        {
            client.BaseAddress = new Uri(fixerApi["ApiUrl"]);
            client.DefaultRequestHeaders.Add("apiKey", fixerApi["ApiKey"]);
        });

        return services;
    }

}
