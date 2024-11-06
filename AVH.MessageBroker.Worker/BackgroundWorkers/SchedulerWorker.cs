using AVH.MessageBroker.Worker.Scheduler.Jobs;
using Quartz;

namespace AVH.MessageBroker.Worker.BackgroundWorkers
{
    internal class SchedulerWorker : BackgroundService
    {
        private readonly ILogger<SchedulerWorker> _logger;
        private readonly IScheduler _scheduler;

        public SchedulerWorker(
            ILogger<SchedulerWorker> logger,
            ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start scheduler workers...");
            var jobKey = nameof(HelloWorldJob);
            var jobGroup = "AVH";

            var job = JobBuilder.Create<HelloWorldJob>()
                .WithIdentity(jobKey, jobGroup)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobKey}-trigger", $"{jobGroup}-trigger")
                .WithCronSchedule("0 0/1 * * * ?")
                .Build();

            var result = await _scheduler.ScheduleJob(job, trigger);
            Console.WriteLine(result);

            await Task.CompletedTask;

        }
    }
}