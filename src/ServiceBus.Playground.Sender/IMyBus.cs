namespace MassTransit.Playground.Sender;

public interface IMyBus
{
    Task Publish<T>(T message, CancellationToken cancellationToken = default);
    Task Publish<T>(T message, TimeSpan span, CancellationToken cancellationToken = default);
    Task PublishBatch<T>(IEnumerable<T> message, CancellationToken cancellationToken = default)
        where T : class;
    Task Send<T>(string destinationUrl, T message, CancellationToken cancellationToken = default);
    Task SendBatch<T>(
        string destinationUrl,
        IEnumerable<T> message,
        CancellationToken cancellationToken = default
    )
        where T : class;
}
