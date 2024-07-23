using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Sender.Observer;

/// <summary>
/// both for send and batch send
/// </summary>
/// <param name="logger"></param>
public class SendObserver(ILogger<SendObserver> logger) : ISendObserver
{
    public async Task PostSend<T>(SendContext<T> context)
        where T : class
    {
        // called right before the message is published (sent to exchange or topic)
        // called before the consumer's Consume method is called
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Trying to send message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    public async Task PreSend<T>(SendContext<T> context)
        where T : class
    {
        // called after the message is published (and acked by the broker if RabbitMQ)
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Sent message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    public async Task SendFault<T>(SendContext<T> context, Exception exception)
        where T : class
    {
        // called if there was an exception publishing the message
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogError(
            exception,
            "Unable to send message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }
}
