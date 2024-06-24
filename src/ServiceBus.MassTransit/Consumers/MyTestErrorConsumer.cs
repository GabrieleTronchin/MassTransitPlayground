using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

public class MyTestErrorConsumer : IConsumer<MyTestErrorMessage>, IConsumer<Fault<MyTestErrorMessage>>
{ 
    readonly ILogger _logger;

    public MyTestErrorConsumer(ILogger<MyTestErrorConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<MyTestErrorMessage> context)
    {
        _logger.LogInformation(
            "New message received: {Id} {time}",
            context.Message.Id,
            context.Message.Time.ToString()
        );
        throw new InvalidOperationException();
    }

    public async Task Consume(ConsumeContext<Fault<MyTestErrorMessage>> context)
    {
        _logger.LogError("New message received: {Id} {time}",
                  context.Message.Message,
                  context.Message.Timestamp.ToString()
              );
    }

}