using MassTransit;
using Microsoft.Extensions.Logging;
using ServiceBus.MassTransit.Messages;

namespace ServiceBus.MassTransit.Receivers;

public class MyTestErrorConsumer : IConsumer<MyTestMessage>
{
    readonly ILogger _logger;

    public MyTestErrorConsumer(ILogger<MyTestErrorConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MyTestMessage> context)
    {
        _logger.LogInformation(
            "New message received: {Id} {time}",
            context.Message.Id,
            context.Message.Time.ToString()
        );
        throw new InvalidOperationException();
    }
}
