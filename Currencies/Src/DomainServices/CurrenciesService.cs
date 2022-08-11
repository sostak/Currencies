using Currencies.Dtos;
using Currencies.Entities;
using Currencies.Infrastructure;
using EasyNetQ;
using Newtonsoft;

namespace Currencies.DomainServices
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly ICurrenciesRepository _currenciesRepository;

        public CurrenciesService(ICurrenciesRepository repository)
        {
            _currenciesRepository = repository;
        }

        public void ForceUpdateRates()
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost"))
            {
                bus.PubSub.Publish("UpdateData");
            }
        }

        public List<CurrencyDto> GetCurrencies()
        {
            return _currenciesRepository.ReadCurrencies();
        }

        public List<CurrencyDto> GetCurrencies(string code)
        {
            return _currenciesRepository.ReadCurrency(code);
        }
    }
}
