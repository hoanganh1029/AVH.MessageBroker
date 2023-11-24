using AVH.MessageBroker.AzureMessaging.QueueServices;
using AVH.MessageBroker.AzureMessaging.QueueServices.SenderReceiver;
using AVH.MessageBroker.Common.Constants;
using AVH.MessageBroker.Common.Extensions;
using AVH.MessageBroker.Models.Commands;
using AVH.MessageBroker.Models.Models;

namespace AVH.MessageBroker.Sender
{
    public class SenderWorker : BackgroundService
    {
        private readonly ILogger<SenderWorker> _logger;
        private readonly ISenderReceiverQueueService<CommandMessage> _senderReceiverQueueService;


        public SenderWorker(
            ILogger<SenderWorker> logger,
             IServiceBusQueueServiceFactory serviceBusQueueServiceFactory)
        {
            _logger = logger;
            _senderReceiverQueueService = serviceBusQueueServiceFactory.CreateSenderReceiverQueueService<CommandMessage>(QueueConstant.QueueName.General);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var exportExcelCommand = new ExportExcelCommand()
            {
                Data = new List<Product>()
                {
                    new Product
                    {
                        Id= "1",
                        Name = "Keyboard"
                    }
                }
            };

            var exportPdfCommand = new ExportPdfCommand()
            {
                Title = "Report Oct",
                Html = "HTML page"
            };

            var exportCommands = new List<CommandMessage>
            {
                new CommandMessage{
                 Id= "1",
                 CommandType = exportExcelCommand.GetType().AssemblyQualifiedName!,
                 CommandText = SerializerHelper.Serialize(exportExcelCommand)
                },
                new CommandMessage{
                 Id= "2",
                 CommandType = exportPdfCommand.GetType().AssemblyQualifiedName!,
                 CommandText = SerializerHelper.Serialize(exportPdfCommand)
                }
            };

            await _senderReceiverQueueService.EnqueueAsync(exportCommands);

            _logger.LogInformation("Enqueue messages successfully");
        }
    }
}