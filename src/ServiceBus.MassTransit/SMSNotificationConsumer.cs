using MassTransit;
using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers;

public class SMSNotificationConsumer : IConsumer<NotificationMessage>
{
    readonly ILogger _logger;

    public SMSNotificationConsumer(ILogger<MailNotificationConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationMessage> context)
    {
        _logger.LogInformation(
            "SMS - New Notification Message: {id}-{title}-{message}",
            context.Message.Id,
            context.Message.Title,
            context.Message.Text
        );
    }
}
