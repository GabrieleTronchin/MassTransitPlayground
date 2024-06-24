using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

public class MailNotificationConsumer : IConsumer<NotificationMessage>
{
    readonly ILogger _logger;

    public MailNotificationConsumer(ILogger<MailNotificationConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationMessage> context)
    {
        _logger.LogInformation(
            "MAIL - New Notification Message: {id}-{title}-{message}",
            context.Message.Id,
            context.Message.Title,
            context.Message.Text
        );
    }
}
