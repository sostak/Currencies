using CurrencyWorker.Entities;
using EasyNetQ;
using Newtonsoft;

namespace CurrencyWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ApplicationServices appService;

        public Worker(ILogger<Worker> logger, ApplicationServices service)
        {
            _logger = logger;
            appService = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if(DateTime.UtcNow.Hour == 0 && DateTime.UtcNow.Minute == 0)
                {
                    appService.InsertDataToDatabase();
                    _logger.LogInformation("Added data to database at {0}", DateTime.UtcNow);

                    await Task.Delay(TimeSpan.FromMinutes(1));

                    appService.RemoveDataFromDatabase(7);
                    _logger.LogInformation("Removed old data from database at {0}", DateTime.UtcNow);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}