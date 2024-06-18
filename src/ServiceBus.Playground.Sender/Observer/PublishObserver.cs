using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ServiceBus.Playground.Sender.Observer;

public class PublishObserver(ILogger<PublishObserver> logger) : IPublishObserver
{
    public async Task PrePublish<T>(PublishContext<T> context)
        where T : class
    {
        // called right before the message is published (sent to exchange or topic)
        // called before the consumer's Consume method is called
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Trying to publish message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    public async Task PostPublish<T>(PublishContext<T> context)
        where T : class
    {
        // called after the message is published (and acked by the broker if RabbitMQ)
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogInformation(
            "Published message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }

    public async Task PublishFault<T>(PublishContext<T> context, Exception exception)
        where T : class
    {
        // called if there was an exception publishing the message
        var message = JsonSerializer.Serialize(context.Message);
        var messageType = context.SupportedMessageTypes.FirstOrDefault();
        logger.LogError(
            exception,
            "Unable to publish message with {correlationId}-{body}-{type}",
            context.CorrelationId,
            message,
            messageType
        );
    }
}
