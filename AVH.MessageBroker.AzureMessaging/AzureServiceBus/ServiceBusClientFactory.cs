using AVH.MessageBroker.AzureMessaging.Configurations;
using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace AVH.MessageBroker.AzureMessaging.AzureServiceBus
{
    public class ServiceBusClientFactory : IServiceBusClientFactory
    {
        private readonly string _connection;
        public ServiceBusClientFactory(IConfiguration configuration)
        {
            var azureServiceOption = configuration.GetSection(nameof(AzureServiceOption)).Get<AzureServiceOption>();
            _connection = azureServiceOption.ConnectionString;
        }

        public async Task<ServiceBusClient> Build(string queueName)
        {
            var serviceBusAdminClient = new ServiceBusAdministrationClient(_connection);
            if (!await serviceBusAdminClient.QueueExistsAsync(queueName))
            {
                await serviceBusAdminClient.CreateQueueAsync(queueName);
            }

            var options = GetServiceBusOption();
            var serviceBusClient = new ServiceBusClient(_connection, options);

            return serviceBusClient;
        }

        private static ServiceBusClientOptions GetServiceBusOption()
        {
            return new ServiceBusClientOptions()
            {
                RetryOptions = new ServiceBusRetryOptions()
                {
                    Mode = ServiceBusRetryMode.Exponential,
                    Delay = TimeSpan.FromSeconds(5),
                    MaxRetries = 3,
                    MaxDelay = TimeSpan.FromMinutes(1)
                }
            };
        }
    }
}
