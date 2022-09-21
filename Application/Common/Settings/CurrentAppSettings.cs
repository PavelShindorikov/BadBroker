namespace Application.Common.Settings;

public class CurrentAppSettings
{
    public int BrokerFee { get; set; }
    public string BaseCurrency { get; set; } = null!;
    public string ExchangeCurrencies { get; set; } = null!;
    public FixerApiSettings FixerApiSettings { get; set; } = new FixerApiSettings();
}
