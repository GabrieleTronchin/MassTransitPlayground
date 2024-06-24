using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;



public class MyTestSendMessageConsumer(ILogger<MyTestSendMessageConsumer> logger)
    : IConsumer<MyTestSendMessage>
{
    public async Task Consume(ConsumeContext<MyTestSendMessage> context)
    {
        logger.LogInformation(
            "New message received: {Id} {time}",
            context.Message.Id,
            context.Message.Time.ToString()
        );
    }
}
