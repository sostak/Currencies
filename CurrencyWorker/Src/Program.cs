using CurrencyWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHostedService<RabbitMessageListener>();
        services.AddSingleton<ApplicationServices>();
    })
    .Build();

await host.RunAsync();
