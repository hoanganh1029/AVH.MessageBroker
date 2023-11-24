using Azure.Messaging.ServiceBus;

namespace AVH.MessageBroker.AzureMessaging.AzureServiceBus
{
    public interface IServiceBusClientFactory
    {
        Task<ServiceBusClient> Build(string queueName);
    }
}
