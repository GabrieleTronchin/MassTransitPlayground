
namespace ServiceBus.Playground.Sender
{
    public interface IMyBus
    {
        Task Publish<T>(T message, CancellationToken cancellationToken = default);
        Task Publish<T>(T message, TimeSpan span, CancellationToken cancellationToken = default);
        Task PublishBatch<T>(IEnumerable<T> message, CancellationToken cancellationToken = default) where T : class;
    }
}