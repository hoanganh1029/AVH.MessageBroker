namespace AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver
{
    public interface ISenderReceiverQueueService<T> where T : class
    {
        Task EnqueueAsync(T obj, string sessionId = "");

        Task EnqueueAsync(IEnumerable<T> arrObj, string sessionId = "");

        Task<T?> DequeueSingleAsync();

        Task<IEnumerable<T?>> DequeueManyAsync(int numberOfMessage);

        Task<T?> DequeueBySessionAsync(string sessionId);

        Task<IEnumerable<T?>> DequeueManyBySessionAsync(string sessionId);
    }
}
