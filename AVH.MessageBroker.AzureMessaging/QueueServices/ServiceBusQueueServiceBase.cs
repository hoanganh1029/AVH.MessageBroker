using AVH.MessageBroker.AzureMessaging.AzureServiceBus;
using Azure.Messaging.ServiceBus;

namespace AVH.MessageBroker.AzureMessaging.QueueServices
{
    public class ServiceBusQueueServiceBase
    {
        protected readonly string _queueName;

        protected readonly ServiceBusClient _serviceBusClient;

        public ServiceBusQueueServiceBase(
            string queueName,
            ServiceBusClient serviceBusClient)
        {
            _queueName = queueName;
            _serviceBusClient = serviceBusClient;
        }
    }
}
