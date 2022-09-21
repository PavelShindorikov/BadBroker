using System.Runtime.Serialization;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Application.Common.Exceptions;

namespace Infrastructure.Services;

/// <summary>
/// External Api Service (https://api.apilayer.com/fixer/)
/// </summary>
public class FixerApiService : IExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<FixerApiService> _logger;

    public FixerApiService(
        HttpClient httpClient,
        ILogger<FixerApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="baseCurrency"></param>
    /// <param name="exchangeCurrencies"></param>
    /// <returns></returns>
    public async Task<TimeSeriesRates> GetRatesByTimeSeries(DateTime startDate, DateTime endDate,
        string baseCurrency, string exchangeCurrencies)
    {
        var responseBody = string.Empty;
        try
        {
            var query = new Dictionary<string, string>
            {
                { "start_date", startDate.ToString("yyyy-MM-dd") },
                { "end_date", endDate.ToString("yyyy-MM-dd") },
                { "base", baseCurrency },
                { "symbols", exchangeCurrencies }
            };
            var response = await _httpClient.GetWithQueryAsync("timeseries?", query);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TimeSeriesRates>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result == null) throw new SerializationException();
            return result;
        }
        catch (HttpRequestException e)
        {
            var error = JsonSerializer.Deserialize<ErrorModel>(responseBody);
            throw new HttpServiceException($"API error. Status code: {error!.Status}. Message: {error.Message}. \nDescription: {error.Description}", e);
        }
        catch (SerializationException)
        {
            throw new HttpServiceException($"Can not deserialize response body to {nameof(TimeSeriesRates)}");
            
        }
    }
}
