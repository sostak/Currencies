using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CurrencyWorker.Entities;
using Newtonsoft.Json;
using Npgsql;

namespace CurrencyWorker
{
    public class ApplicationServices
    {
        public void HandleMessages(string message)
        {
            if(message == "UpdateData")
            {
                InsertDataToDatabase();
                Console.WriteLine("Forced rates update at {0}", DateTime.UtcNow);
            }
        }
        public void InsertDataToDatabase()
        {
            string url = "https://v6.exchangerate-api.com/v6/API_KEY/latest/EUR";
            ThirdPartyAPIContract rates = GetData(url);

            string cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
            SqlInsert(cs, rates.conversion_rates);
        }
        public void RemoveDataFromDatabase(int olderThan)
        {
            string cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

            SqlDelete(cs, olderThan);
        }
        private ThirdPartyAPIContract GetData(string url)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(url);
                ThirdPartyAPIContract rates = JsonConvert.DeserializeObject<ThirdPartyAPIContract>(json);
                return rates;
            }
        }

        private void SqlInsert(string cs, ConversionRate rates)
        {
            using var connection = new NpgsqlConnection(cs);
            connection.Open();

            Type type = rates.GetType();

            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            PropertyInfo[] properties = type.GetProperties(flags);

            foreach (PropertyInfo property in properties)
            {
                string name = property.Name;
                string rate = property.GetValue(rates, null).ToString().Replace(',', '.');
                string sql = String.Format("INSERT INTO project.currencies(code, rate, date) VALUES('{0}', '{1}', '{2}');", name, rate, DateTime.UtcNow);
                var command = new NpgsqlCommand(sql, connection);
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        private void SqlDelete(string cs, int olderThan)
        {
            using var connection = new NpgsqlConnection(cs);
            connection.Open();

            string sql = String.Format("DELETE FROM project.currencies WHERE date < current_date at time zone 'UTC' - interval '{0} days';", olderThan);
            
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
