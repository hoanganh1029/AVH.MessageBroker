using AVH.MessageBroker.Models.Commands;
using Microsoft.Extensions.Logging;

namespace AVH.MessageBroker.Services.Handlers.Export
{
    public class ExportPdfHandler : CommandHandler<ExportPdfCommand>
    {
        private readonly ILogger<ExportPdfHandler> _logger;


        public ExportPdfHandler(ILogger<ExportPdfHandler> logger)
        {
            _logger = logger;
        }

        public override async Task ExecuteAsync(ExportPdfCommand command)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation("Run command {commandType}, data: '{commandText}'", nameof(ExportPdfCommand), command.Html);
            });
        }
    }
}
