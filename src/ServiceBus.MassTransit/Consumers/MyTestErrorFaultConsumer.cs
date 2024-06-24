using MassTransit.Playground.Messages;
using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

public class MyTestErrorFaultConsumer(ILogger<MyTestErrorFaultConsumer> logger) : IConsumer<Fault<MyTestErrorMessage>>
{ 
    public async Task Consume(ConsumeContext<Fault<MyTestErrorMessage>> context)
    {
        logger.LogError("An error occurred processing message: {Id} {time} {exceptionMsg}",
                  context.Message.Message.Id,
                  context.Message.Message.Time,
                  context.Message.Exceptions.First().Message
              );
    }

}