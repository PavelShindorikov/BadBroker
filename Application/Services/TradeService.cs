using Application.Common.Exceptions;
using Application.Contracts;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Mappings;
using Application.Common.Settings;
using Microsoft.Extensions.Options;

namespace Application.Services;

/// <summary>
/// Trade service 
/// </summary>
public class TradeService : ITradeService
{
    private readonly IExternalApiService _apiClient;
    private readonly ISearchService _searchService;
    private readonly CurrentAppSettings _appSettings;
    private readonly IRateRepository _repository;

    public TradeService(
        IExternalApiService fixerApiClient,
        ISearchService searchService,
        IOptions<CurrentAppSettings> options,
        IRateRepository ratesRepository)
    {
        _apiClient = fixerApiClient;
        _searchService = searchService;
        _appSettings = options.Value;
        _repository = ratesRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<BestRevenueResponse> GetBestRevenueAsync(BestRevenueRequest request)
    {
        try
        {
            if (_appSettings == null) throw new NullReferenceException("AppSettings is null");
            var baseCurrency = _appSettings.BaseCurrency;
            var exchangeCurrencies = _appSettings.ExchangeCurrencies;
            var brokerFee = _appSettings.BrokerFee;

            var startDate = request.StartDate;
            var endDate = request.EndDate;
            var money = request.Money;

            var eachDate = startDate.EachDayTo(endDate);

            //get rates from database cache
            var cachedRates = await _repository.GetRatesAsync(baseCurrency, startDate, endDate);
            var dateForApi = eachDate.Except(cachedRates.Select(d => d.Date));

            var apiRates = new List<RateDto>();
            if (dateForApi.Any())
            {
                var intervals = dateForApi.OrderBy(d => d.Date).ToTimeIntervals();

                //get rates from api
                var apiTasks = intervals
                    .Select(i => _apiClient.GetRatesByTimeSeries(i.Start, i.End, baseCurrency, exchangeCurrencies))
                    .ToList();
                apiRates = await Task.WhenAll(apiTasks)
                    .ContinueWith(t => t.Result
                        .SelectMany(r => r.ToRatesDto())
                        .ToList()
                    );

                //save rates from apy to database cache
                await _repository.AddRatesAsync(apiRates.ToBaseCurrencies(baseCurrency));
            }

            var rates = apiRates
                .Concat(cachedRates.ToRatesDto())
                .OrderBy(d => d.Date)
                .ToList();

            //get best revenue
            var bestRevenue = _searchService.SearchBestRevenue(rates, money, brokerFee);
            bestRevenue.Rates = rates.ToRatesResponse();
            
            return bestRevenue;
        }
        catch (NullReferenceException ex)
        {
            throw new RateServiceException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new RateServiceException(ex.Message, ex);
        }
    }
}