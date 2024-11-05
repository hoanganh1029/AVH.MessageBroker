using AVH.MessageBroker.AzureMessaging.AzureServiceBus;
using AVH.MessageBroker.AzureMessaging.Extensions;
using AVH.MessageBroker.Common.Extensions;
using Azure.Messaging.ServiceBus;

namespace AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver
{
    public class SenderReceiverQueueService<T> : ServiceBusQueueServiceBase, ISenderReceiverQueueService<T> where T : class
    {
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusReceiver _receiver;

        private readonly ILogger<SenderReceiverQueueService<T>> _logger;

        private readonly ServiceBusReceiverOptions _serviceBusReceiverOptions = new()
        {
            ReceiveMode = ServiceBusReceiveMode.PeekLock,
            //PrefetchCount
            //SubQueue
        };

        public SenderReceiverQueueService(
            string queueName,
            ServiceBusClient serviceBusClient,
            ILogger<SenderReceiverQueueService<T>> logger)
            : base(queueName, serviceBusClient)
        {
            _logger = logger;
            _sender = _serviceBusClient.CreateSender(_queueName);
            _receiver = _serviceBusClient.CreateReceiver(_queueName, _serviceBusReceiverOptions);
        }

        #region Enqueue
        public async Task EnqueueAsync(T obj, string sessionId = "")
        {
            var serviceBusMessage = new ServiceBusMessage(SerializerHelper.Serialize(obj))
            {
                SessionId = string.IsNullOrEmpty(sessionId) ? nameof(T) : sessionId
            };
            await _sender.SendMessageAsync(serviceBusMessage);
        }

        public async Task EnqueueAsync(IEnumerable<T> arrObj, string sessionId = "")
        {
            var serviceBusMessages = arrObj.Select(obj =>
                new ServiceBusMessage(SerializerHelper.Serialize(obj))
                {
                    SessionId = string.IsNullOrEmpty(sessionId) ? nameof(T) : sessionId
                });
            await _sender.SendMessagesAsync(serviceBusMessages);
        }

        #endregion Enqueue

        #region Dequeue

        public async Task<T?> DequeueSingleAsync()
        {
            var receivedMessage = await _receiver.ReceiveMessageAsync();
            return receivedMessage.ToObject<T>();
        }

        public async Task<IEnumerable<T?>> DequeueManyAsync(int numberOfMessage)
        {
            var receivedMessages = await _receiver.ReceiveMessagesAsync(numberOfMessage);
            return receivedMessages.Select(x => x.ToObject<T>());
        }

        public async Task<T?> DequeueBySessionAsync(string sessionId)
        {
            var receiverBySession = await _serviceBusClient.AcceptSessionAsync(_queueName, sessionId);
            var receivedMessage = await receiverBySession.ReceiveMessageAsync();
            if (receivedMessage == null)
            {
                _logger.LogError("The receive message in DequeueBySessionAsync is null");
                return default;
            }
            return receivedMessage.ToObject<T>();
        }

        public async Task<IEnumerable<T?>> DequeueManyBySessionAsync(string sessionId)
        {
            var receiverBySession = await _serviceBusClient.AcceptSessionAsync(_queueName, sessionId);
            var receivedMessages = receiverBySession.ReceiveMessagesAsync();
            if (receivedMessages == null)
            {
                return Enumerable.Empty<T>();
            }
            else
            {
                var enumerator = receivedMessages.GetAsyncEnumerator();
                var lstObj = new List<T>();
                while (await enumerator.MoveNextAsync())
                {
                    var obj = enumerator.Current.ToObject<T>();
                    if (obj != null)
                    {
                        lstObj.Add(obj);
                    }
                }
                return lstObj;
            }
        }

        #endregion Dequeue
    }
}
