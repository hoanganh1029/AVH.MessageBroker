using AVH.MessageBroker.Common.Extensions;
using Azure.Messaging.ServiceBus;

namespace AVH.MessageBroker.AzureMessaging.Extensions
{
    public static class ServiceBusReceivedMessageExtensions
    {
        public static T? ToObject<T>(this ServiceBusReceivedMessage message)
        {
            var messageBody = message.Body.ToString();
            return SerializerHelper.Deserialize<T>(messageBody);
        }
    }
}
