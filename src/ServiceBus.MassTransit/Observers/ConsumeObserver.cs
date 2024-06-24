using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Observers
{
    /// <summary>
    /// Sample of consumer for a specific entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="logger"></param>
    public class ConsumeObserver<T>(ILogger<ConsumeObserver<T>> logger) : IConsumeMessageObserver<T>
        where T : class
    {
        async Task IConsumeMessageObserver<T>.PreConsume(ConsumeContext<T> context)
        {
            // called before the consumer's Consume method is called
            logger.LogInformation("PreConsume called.");
        }

        async Task IConsumeMessageObserver<T>.PostConsume(ConsumeContext<T> context)
        {
            // called after the consumer's Consume method was called
            // again, exceptions call the Fault method.
            logger.LogInformation("PostConsume called.");
        }

        async Task IConsumeMessageObserver<T>.ConsumeFault(
            ConsumeContext<T> context,
            Exception exception
        )
        {
            // called when a consumer throws an exception consuming the message
            logger.LogInformation("ConsumeFault called.");
        }
    }
}
