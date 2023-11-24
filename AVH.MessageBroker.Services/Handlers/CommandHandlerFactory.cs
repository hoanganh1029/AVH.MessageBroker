using Microsoft.Extensions.DependencyInjection;

namespace AVH.MessageBroker.Services.Handlers
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommandHandler Create(Type commandType)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
            if (_serviceProvider.GetServices(handlerType) is IEnumerable<ICommandHandler> handlers && handlers.Any())
            {
                return handlers.First();
            }

            return new DefaultCommandHandler();
        }

    }
}
