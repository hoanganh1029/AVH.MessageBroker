using AVH.MessageBroker.AzureMessaging.QueueServices.Processor;
using AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver;

namespace AVH.MessageBroker.AzureMessaging.QueueServices
{
    public interface IServiceBusQueueServiceFactory
    {
        SenderReceiverQueueService<T> CreateSenderReceiverQueueService<T>(string queueName) where T : class;

        ProcessorQueueService<T> CreateProcessorQueueService<T>(string queueName) where T : class;
    }
}
