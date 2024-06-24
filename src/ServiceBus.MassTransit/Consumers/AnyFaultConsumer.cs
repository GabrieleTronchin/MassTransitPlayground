using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Consumers;

/// <summary>
/// Any Fault consumer works for every fault without know it type
/// </summary>
/// <param name="logger"></param>
public class AnyFaultConsumer(ILogger<AnyFaultConsumer> logger) : IConsumer<Fault>
{
    public async Task Consume(ConsumeContext<Fault> context)
    {
        logger.LogError(
            $"An error occurred processing messages: {string.Join(",", context.Message.Exceptions.Select(x => x.Message))}"
        );
    }
}
