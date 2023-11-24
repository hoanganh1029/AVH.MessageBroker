using AVH.MessageBroker.AzureMessaging.QueueServices;
using AVH.MessageBroker.AzureMessaging.QueueServices.Processor;
using AVH.MessageBroker.Common.Constants;
using AVH.MessageBroker.Common.Extensions;
using AVH.MessageBroker.Models.Commands;
using AVH.MessageBroker.Services.Handlers;

namespace AVH.MessageBroker.Worker.BackgroundWorkers
{
    public class GeneralCommandWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<GeneralCommandWorker> _logger;
        private readonly IProcessorQueueService<CommandMessage> _processorCommandQueueService;

        public GeneralCommandWorker(
            IServiceProvider serviceProvider,
            ILogger<GeneralCommandWorker> logger,
            IServiceBusQueueServiceFactory serviceBusQueueServiceFactory   
            )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _processorCommandQueueService = serviceBusQueueServiceFactory.CreateProcessorQueueService<CommandMessage>(QueueConstant.QueueName.General);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processorCommandQueueService.CreateProcessor(
                processMessageFunc: async (commandMessage) =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    var commandHandlerFactory = scope.ServiceProvider.GetRequiredService<ICommandHandlerFactory>();

                    var commandType = Type.GetType(commandMessage.CommandType);
                    var handler = commandHandlerFactory.Create(commandType);
                    var commandObj = SerializerHelper.Deserialize(commandMessage.CommandText, commandType);

                    await handler.ExecuteAsync(commandObj);

                    _logger.LogInformation("Process message in {queueName} queue: {message}", QueueConstant.QueueName.General, commandMessage);                    
                },
                processErrorFunc: (ex) =>
                {
                    _logger.LogError("Exception from General queue", ex.Message);
                    return Task.CompletedTask;

                });

            _logger.LogInformation("{workerName} has been created successfully", nameof(GeneralCommandWorker));
            return Task.CompletedTask;
        }
    }
}
