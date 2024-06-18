using MassTransit;
using Microsoft.Extensions.Logging;
using ServiceBus.MassTransit.Messages;

namespace ServiceBus.MassTransit.Receivers;

class BatchMessageConsumer(ILogger<MyTestErrorConsumer> logger)
    : IConsumer<Batch<MyTestBatchMessage>>
{
    public async Task Consume(ConsumeContext<Batch<MyTestBatchMessage>> context)
    {
        for (int i = 0; i < context.Message.Length; i++)
        {
            var message = context.Message[i].Message;
            logger.LogInformation(
                "new message received: {Id} {time}",
                message.Id,
                message.Time.ToString()
            );
        }
    }
}
