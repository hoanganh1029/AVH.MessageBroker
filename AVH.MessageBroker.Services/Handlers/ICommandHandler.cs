namespace AVH.MessageBroker.Services.Handlers
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(object command);
    }

    public interface ICommandHandler<in T> : ICommandHandler where T : class
    {
        Task ExecuteAsync(T command);
    }
}
