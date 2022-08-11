using Currencies.Dtos;

namespace Currencies.Entities
{
    public class Currency
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public double Rate { get; init; }
        public DateTime Date { get; init; }
        public int ID { get; init; }
    }
}
