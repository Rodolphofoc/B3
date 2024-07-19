using Microsoft.Extensions.Hosting;
using TaskManagerConsumer.Interface;

namespace TaskManagerConsumer.Settings
{
    public class QueueConsumerBackgroundService : BackgroundService
    {
        private readonly IQueueConsumerService _queueConsumerService;

        public QueueConsumerBackgroundService(IQueueConsumerService queueConsumerService)
        {
            _queueConsumerService = queueConsumerService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _queueConsumerService.StartConsuming();
        }
    }
}
