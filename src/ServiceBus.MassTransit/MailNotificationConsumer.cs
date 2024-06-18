using MassTransit;
using Microsoft.Extensions.Logging;
using ServiceBus.MassTransit.Messages;

namespace ServiceBus.MassTransit.Receivers;

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
