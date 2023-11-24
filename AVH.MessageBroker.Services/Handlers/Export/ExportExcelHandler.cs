using AVH.MessageBroker.Models.Commands;
using Microsoft.Extensions.Logging;

namespace AVH.MessageBroker.Services.Handlers.Export
{
    public class ExportExcelHandler : CommandHandler<ExportExcelCommand>
    {
        private readonly ILogger<ExportExcelHandler> _logger;


        public ExportExcelHandler(ILogger<ExportExcelHandler> logger)
        {
            _logger = logger;
        }

        public override async Task ExecuteAsync(ExportExcelCommand command)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation("Run command {commandType}, data: '{commandText}'", nameof(ExportExcelCommand), command.Data);
            });
        }
    }
}
