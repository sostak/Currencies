using Currencies.Dtos;
using Currencies.Entities;

namespace Currencies
{
    public static class TaskUtils
    {
        public static CurrencyDto AsDto(this Currency currency)
        {
            CurrencyDto dto = new CurrencyDto()
            {
                Name = currency.Name,
                Code = currency.Code,
                Rate = currency.Rate,
                Date = currency.Date
            };

            return dto;
        }
    }
}
