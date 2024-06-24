using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

class BatchMessageConsumer(ILogger<MyTestErrorConsumer> logger)
    : IConsumer<Batch<MyTestBatchMessage>>
{
    public async Task Consume(ConsumeContext<Batch<MyTestBatchMessage>> context)
    {
        for (int i = 0; i < context.Message.Length; i++)
        {
            var message = context.Message[i].Message;
            logger.LogInformation(
                "New message received: {Id} {time}",
                message.Id,
                message.Time.ToString()
            );
        }
    }
}
