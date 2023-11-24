using Microsoft.Extensions.Logging;

namespace AVH.MessageBroker.Services.Handlers
{
    public abstract class CommandHandler<T> : ICommandHandler<T> where T : class
    {    
        public abstract Task ExecuteAsync(T command);

        public Task ExecuteAsync(object command)
        {
            return ExecuteAsync((T)command);
        }
    }
}
