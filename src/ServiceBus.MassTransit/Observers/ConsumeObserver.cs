using MassTransit;
using Microsoft.Extensions.Logging;

namespace ServiceBus.MassTransit.Receivers.Observers
{
    public class ConsumeObserver<T>(ILogger<ConsumeObserver<T>> logger) : IConsumeMessageObserver<T>
        where T : class
    {
        async Task IConsumeMessageObserver<T>.PreConsume(ConsumeContext<T> context)
        {
            // called before the consumer's Consume method is called
            logger.LogInformation("");
        }

        async Task IConsumeMessageObserver<T>.PostConsume(ConsumeContext<T> context)
        {
            // called after the consumer's Consume method was called
            // again, exceptions call the Fault method.
            logger.LogInformation("");
        }

        async Task IConsumeMessageObserver<T>.ConsumeFault(
            ConsumeContext<T> context,
            Exception exception
        )
        {
            // called when a consumer throws an exception consuming the message
            logger.LogInformation("");
        }
    }
}
