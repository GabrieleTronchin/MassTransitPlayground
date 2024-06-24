using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

public class MyTestErrorConsumer(ILogger<MyTestErrorConsumer> logger)
    : IConsumer<MyTestErrorMessage>
{
    public async Task Consume(ConsumeContext<MyTestErrorMessage> context)
    {
        logger.LogInformation(
            "New message received: {Id} {time}",
            context.Message.Id,
            context.Message.Time.ToString()
        );
        throw new InvalidOperationException();
    }
}
