using Currencies.Dtos;
using Currencies.Entities;
using Npgsql;
using System.Data;

namespace Currencies.Infrastructure
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        public List<CurrencyDto> ReadCurrencies()
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

            using var connection = new NpgsqlConnection(cs);
            connection.Open();

            var sql = "SELECT name, code, rate, date, id FROM project.currencies; ";

            List<CurrencyDto> currencies = new List<CurrencyDto>();

            using (var command = new NpgsqlCommand(sql, connection))
            {
                var dataAdapter = new NpgsqlDataAdapter(command);

                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    var currency = row;

                    currencies.Add(new Currency()
                    {
                        Name = Convert.ToString(currency["name"]),
                        Code = Convert.ToString(currency["code"]),
                        Rate = Convert.ToDouble(currency["rate"]),
                        Date = Convert.ToDateTime(currency["date"]),
                        ID = Convert.ToInt32(currency["id"])
                    }.AsDto());

                }
            }

            return currencies;
        }

        public List<CurrencyDto> ReadCurrency(string code)
        {
            var cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

            using var connection = new NpgsqlConnection(cs);
            connection.Open();

            var sql = $"SELECT name, code, rate, date, id FROM project.currencies where code = '{code}'; ";

            List<CurrencyDto> currencies = new List<CurrencyDto>();

            using (var command = new NpgsqlCommand(sql, connection))
            {
                var dataAdapter = new NpgsqlDataAdapter(command);

                var dataTable = new DataTable();

                dataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    var currency = row;

                    currencies.Add(new Currency()
                    {
                        Name = Convert.ToString(currency["name"]),
                        Code = Convert.ToString(currency["code"]),
                        Rate = Convert.ToDouble(currency["rate"]),
                        Date = Convert.ToDateTime(currency["date"]),
                        ID = Convert.ToInt32(currency["id"])
                    }.AsDto());

                }
            }

            return currencies;
        }
    }
}
