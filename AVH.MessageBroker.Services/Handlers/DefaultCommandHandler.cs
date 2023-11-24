using AVH.MessageBroker.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace AVH.MessageBroker.Services.Handlers
{
    public class DefaultCommandHandler : ICommandHandler
    {
        public DefaultCommandHandler()
        {
        }

        public async Task ExecuteAsync(object command)
        {
            var loggerFactory = LoggerFactory.Create(x => x.AddConsole());
            var logger = loggerFactory.CreateLogger<DefaultCommandHandler>();
            await Task.Run(() =>
            {
                logger.LogError("Unable to resolve the command message {}", SerializerHelper.Serialize(command));
            });
        }
    }
}
