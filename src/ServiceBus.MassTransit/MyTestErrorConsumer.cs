using MassTransit;
using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers;

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
