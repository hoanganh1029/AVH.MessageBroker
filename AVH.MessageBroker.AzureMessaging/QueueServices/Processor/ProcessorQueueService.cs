using AVH.MessageBroker.AzureMessaging.AzureServiceBus;
using AVH.MessageBroker.Common.Extensions;
using Azure.Messaging.ServiceBus;

namespace AVH.MessageBroker.AzureMessaging.QueueServices.Processor
{
    public class ProcessorQueueService<T> : ServiceBusQueueServiceBase, IProcessorQueueService<T> where T : class
    {
        private readonly ILogger<ProcessorQueueService<T>> _logger;

        public ProcessorQueueService(
            string queueName,
            IServiceBusClientFactory serviceBusClientFactory,
            ILogger<ProcessorQueueService<T>> logger)
            : base(queueName, serviceBusClientFactory)
        {
            _logger = logger;
        }

        public void CreateProcessor(Func<T, Task> processMessageFunc, Func<Exception, Task>? processErrorFunc = default)
        {
            var options = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            };
            var processor = _serviceBusClient.CreateProcessor(_queueName, options);

            processor.ProcessMessageAsync += async (messageEventArgs) =>
            {
                string body = messageEventArgs.Message.Body.ToString();
                _logger.LogInformation("Messsage: {body}", body);

                if (SerializerHelper.TryDeserializeObject<T>(body, out var messageObject))
                {
                    await processMessageFunc(messageObject!);
                }
                else
                {
                    var messageType = typeof(T).FullName;
                    _logger.LogInformation("Can not deserialize to {messageType} from '{body}'", messageType, body);
                }

                //If AutoCompleteMessages = false and this command is not executed, the message will be still exist in queue
                await messageEventArgs.CompleteMessageAsync(messageEventArgs.Message);

            };

            processor.ProcessErrorAsync += async (errorEventArgs) =>
            {
                _logger.LogError(errorEventArgs.Exception, "Processor failed {ErrorSource} {FullyQualifiedNamespace}", errorEventArgs.ErrorSource, errorEventArgs.FullyQualifiedNamespace);
                if (processErrorFunc != null)
                {
                    await processErrorFunc(errorEventArgs.Exception);
                }
            };

            processor.StartProcessingAsync();
        }
    }
}
