using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Observers;

public class GlobalConsumeObserver(ILogger<GlobalConsumeObserver> logger) : IConsumeObserver
{
    async Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
    {
        // called before the consumer's Consume method is called
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Trying to consume message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    async Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
    {
        // called after the consumer's Consume method is called
        // if an exception was thrown, the ConsumeFault method is called instead
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Successfully consumed message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    async Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
    {
        // called if the consumer's Consume method throws an exception
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogError(
            exception,
            "Unable to consume message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }
}
