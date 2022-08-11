using Currencies.Dtos;

namespace Currencies.DomainServices
{
    public interface ICurrenciesService
    {
        List<CurrencyDto> GetCurrencies();
        List<CurrencyDto> GetCurrencies(string code);
        void ForceUpdateRates();
    }
}
