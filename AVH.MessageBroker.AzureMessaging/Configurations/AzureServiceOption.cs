namespace AVH.MessageBroker.AzureMessaging.Configurations
{
    public class AzureServiceOption
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string QueuePrefix { get; set; } = string.Empty;

        public IEnumerable<AzureServiceBusQueue> Queues { get; set; } = new List<AzureServiceBusQueue>();

        public IEnumerable<AzureServiceBusTopic> Topics { get; set; } = new List<AzureServiceBusTopic>();
    }

    public class AzureServiceBusQueue
    {
        public string Name { get; set; } = string.Empty;
        public double MaxDeliveryCount { get; set; }
        public double LockDuration { get; set; }
    }

    public class AzureServiceBusTopic
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<AzureServiceBusSubscription> Subscriptions { get; set; } = new List<AzureServiceBusSubscription>();
    }

    public class AzureServiceBusSubscription
    {
        public string Name { get; set; } = string.Empty;
        public double MaxDeliveryCount { get; set; }
        public double LockDuration { get; set; }
    }
}
