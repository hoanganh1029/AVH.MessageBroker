using Quartz;

namespace AVH.MessageBroker.Worker.Scheduler.Jobs
{
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;

        public HelloWorldJob(ILogger<HelloWorldJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello AVH Job!");
            return Task.CompletedTask;
        }
    }
}