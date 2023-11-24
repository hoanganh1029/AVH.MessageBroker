namespace AVH.MessageBroker.AzureMessaging.QueueServices.Processor
{
    public interface IProcessorQueueService<T> where T : class
    {
        void CreateProcessor(Func<T, Task> processMessageFunc, Func<Exception, Task>? processErrorFunc = default);
    }
}
