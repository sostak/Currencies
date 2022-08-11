using Currencies.Dtos;
using Currencies.Entities;
using Npgsql;
using System.Data;

namespace Currencies.Infrastructure
{
    public interface ICurrenciesRepository
    {
        List<CurrencyDto> ReadCurrencies();
        List<CurrencyDto> ReadCurrency(string code);
    }
}