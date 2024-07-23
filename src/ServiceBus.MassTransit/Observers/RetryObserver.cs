using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Observers
{
    //TODO This class is to test
    internal class RetryObserver(ILogger<RetryObserver> logger) : IRetryObserver
    {
        Task IRetryObserver.PostCreate<T>(RetryPolicyContext<T> context)
        {
            return Task.CompletedTask;
        }

        Task IRetryObserver.PostFault<T>(RetryContext<T> context)
        {
            return Task.CompletedTask;
        }

        Task IRetryObserver.PreRetry<T>(RetryContext<T> context)
        {
            return Task.CompletedTask;
        }

        Task IRetryObserver.RetryComplete<T>(RetryContext<T> context)
        {
            return Task.CompletedTask;
        }

        Task IRetryObserver.RetryFault<T>(RetryContext<T> context)
        {
            return Task.CompletedTask;
        }
    }
}
