using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft;

namespace CurrencyWorker
{
    public class RabbitMessageListener : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ApplicationServices appService;

        public RabbitMessageListener(ILogger<Worker> logger, ApplicationServices service)
        {
            _logger = logger;
            appService = service;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bus = RabbitHutch.CreateBus("host=localhost");
            await bus.PubSub.SubscribeAsync<string>("Message", appService.HandleMessages);
        }
    }
}
