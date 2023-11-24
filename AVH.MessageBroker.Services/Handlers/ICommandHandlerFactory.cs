namespace AVH.MessageBroker.Services.Handlers
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler Create(Type commandType);
    }
}
