using Currencies.Entities;

namespace Currencies.Dtos
{
    public class CurrencyDto
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public double Rate { get; init; }
        public DateTime Date { get; init; }
    }
}
