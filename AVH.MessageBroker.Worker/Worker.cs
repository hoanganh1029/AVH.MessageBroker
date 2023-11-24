using AVH.MessageBroker.AzureMessaging.Configurations;

namespace AVH.MessageBroker.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;        
        public Worker(ILogger<Worker> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            configuration.GetSection(nameof(AzureServiceOption)).Get<AzureServiceOption>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}