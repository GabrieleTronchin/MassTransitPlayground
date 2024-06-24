using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

public class MySampleRequestConsumer(ILogger<MySampleRequestConsumer> logger)
    : IConsumer<MySampleRequest>
{
    public async Task Consume(ConsumeContext<MySampleRequest> context)
    {
        logger.LogInformation("Message Received: {id}", context.Message.id);

        //TODO WORK

        if (context.IsResponseAccepted<MySampleRequestAccepted>())
        {
            await context.RespondAsync(
                new MySampleRequestAccepted(context.Message.id, DateTime.UtcNow)
            );
        }
    }
}
