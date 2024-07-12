namespace MassTransit.Playground.Sender;

public class MyBus(IPublishEndpoint publishEndpoint, IMessageScheduler messageScheduler, IBus bus)
    : IMyBus
{
    /// <summary>
    /// Single consumer, send must pass trough  GetSendEndpoint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task Send<T>(
        string destinationUrl,
        T message,
        CancellationToken cancellationToken = default
    )
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var sendEndpoint = await bus.GetSendEndpoint(new Uri(destinationUrl));

        await sendEndpoint.Send(message, cancellationToken);
    }

    /// <summary>
    /// Multiple message with single consumer, send must pass trough  GetSendEndpoint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task SendBatch<T>(
        string destinationUrl,
        IEnumerable<T> message,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var sendEndpoint = await bus.GetSendEndpoint(new Uri(destinationUrl));

        await sendEndpoint.SendBatch(message, cancellationToken);
    }

    /// <summary>
    /// Multiple consumer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        await publishEndpoint.Publish(message, cancellationToken);
    }

    /// <summary>
    /// Multiple message with multiple consumers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task PublishBatch<T>(
        IEnumerable<T> message,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        await publishEndpoint.PublishBatch(message, cancellationToken);
    }

    /// <summary>
    /// Multiple consumer with schedulers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="span"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task Publish<T>(
        T message,
        TimeSpan span,
        CancellationToken cancellationToken = default
    )
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var scheduledMessage = await messageScheduler.SchedulePublish(
            DateTime.UtcNow.Add(span),
            message,
            cancellationToken
        );
    }
}
