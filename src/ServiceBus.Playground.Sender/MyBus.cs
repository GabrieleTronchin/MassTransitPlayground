using MassTransit;

namespace MassTransit.Playground.Sender
{
    public class MyBus(IPublishEndpoint bus, IMessageScheduler messageScheduler) : IMyBus
    {
        public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            await bus.Publish(message, cancellationToken);
        }


        public async Task PublishBatch<T>(IEnumerable<T> message, CancellationToken cancellationToken = default) where T : class
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            await bus.PublishBatch(message, cancellationToken);
        }


        public async Task Publish<T>(T message, TimeSpan span, CancellationToken cancellationToken = default)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var scheduledMessage = await messageScheduler.SchedulePublish(DateTime.UtcNow.Add(span), message, cancellationToken);
        }
    }
}
