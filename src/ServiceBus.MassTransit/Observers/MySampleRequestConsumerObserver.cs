using MassTransit.Playground.Receivers.Consumers;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Observers;

/// <summary>
/// Sample of consumer for a specific entity
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="logger"></param>
public class MySampleRequestConsumerObserver(ILogger<MySampleRequestConsumerObserver> logger) : IConsumeMessageObserver<MySampleRequestConsumer>
{

    public Task PreConsume(ConsumeContext<MySampleRequestConsumer> context)
    {
        // called before the consumer's Consume method is called
        logger.LogInformation("PreConsume called.");
        return Task.CompletedTask;
    }

    public Task PostConsume(ConsumeContext<MySampleRequestConsumer> context)
    {
        // called after the consumer's Consume method was called
        // again, exceptions call the Fault method.
        logger.LogInformation("PostConsume called.");
        return Task.CompletedTask;
    }

    public Task ConsumeFault(ConsumeContext<MySampleRequestConsumer> context, Exception exception)
    {
        // called when a consumer throws an exception consuming the message
        logger.LogInformation("ConsumeFault called.");
        return Task.CompletedTask;
    }
}
