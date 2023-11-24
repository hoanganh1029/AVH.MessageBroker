using AVH.MessageBroker.AzureMessaging.AzureServiceBus;
using AVH.MessageBroker.AzureMessaging.QueueServices.Processor;
using AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver;
using AVH.MessageBroker.AzureMessaging.QueueServices;
using AVH.MessageBroker.Sender;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient(typeof(IProcessorQueueService<>), typeof(ProcessorQueueService<>));
        services.AddTransient(typeof(ISenderReceiverQueueService<>), typeof(SenderReceiverQueueService<>));
        services.AddSingleton<IServiceBusQueueServiceFactory, ServiceBusQueueServiceFactory>();
        services.AddSingleton<IServiceBusClientFactory, ServiceBusClientFactory>();

        services.AddHostedService<SenderWorker>();
    })
    .ConfigureAppConfiguration((hostContext, configBuilder) =>
    {
        configBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .Build();
    })
    .Build();

await host.RunAsync();
