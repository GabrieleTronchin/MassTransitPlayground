using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Receivers.Observers
{
    internal class RetryObserver(ILogger<RetryObserver> logger) : IRetryObserver
    {
        Task IRetryObserver.PostCreate<T>(RetryPolicyContext<T> context)
        {
            throw new NotImplementedException();
        }

        Task IRetryObserver.PostFault<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }

        Task IRetryObserver.PreRetry<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }

        Task IRetryObserver.RetryComplete<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }

        Task IRetryObserver.RetryFault<T>(RetryContext<T> context)
        {
            throw new NotImplementedException();
        }
    }
}
