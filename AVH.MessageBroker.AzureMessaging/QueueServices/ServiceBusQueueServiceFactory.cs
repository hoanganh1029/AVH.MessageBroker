using AVH.MessageBroker.AzureMessaging.AzureServiceBus;
using AVH.MessageBroker.AzureMessaging.Configurations;
using AVH.MessageBroker.AzureMessaging.QueueServices.Processor;
using AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver;

namespace AVH.MessageBroker.AzureMessaging.QueueServices
{
    public class ServiceBusQueueServiceFactory : IServiceBusQueueServiceFactory
    {
        private readonly IServiceBusClientFactory _serviceBusClientFactory;
        private readonly ILoggerFactory _loggerFactory;

        private readonly string _queuePrefix;

        public ServiceBusQueueServiceFactory(
            IServiceBusClientFactory serviceBusClientFactory,
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            _serviceBusClientFactory = serviceBusClientFactory;
            var azureServiceOption = configuration.GetSection(nameof(AzureServiceOption)).Get<AzureServiceOption>();
            _queuePrefix = azureServiceOption.QueuePrefix;
            _loggerFactory = loggerFactory;
        }

        public SenderReceiverQueueService<T> CreateSenderReceiverQueueService<T>(string queueName) where T : class
        {
            var logger = _loggerFactory.CreateLogger<SenderReceiverQueueService<T>>();
            var fullQueueName = $"{_queuePrefix}-{queueName}";
            var serviceBusClient = _serviceBusClientFactory.Build(queueName).GetAwaiter().GetResult();
            return new SenderReceiverQueueService<T>(fullQueueName, serviceBusClient, logger);
        }

        public ProcessorQueueService<T> CreateProcessorQueueService<T>(string queueName) where T : class
        {
            var logger = _loggerFactory.CreateLogger<ProcessorQueueService<T>>();
            var fullQueueName = $"{_queuePrefix}-{queueName}";
            var serviceBusClient = _serviceBusClientFactory.Build(queueName).GetAwaiter().GetResult();
            return new ProcessorQueueService<T>(fullQueueName, serviceBusClient, logger);
        }
    }
}
