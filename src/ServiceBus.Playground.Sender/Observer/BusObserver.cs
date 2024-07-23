using Microsoft.Extensions.Logging;

namespace MassTransit.Playground.Sender.Observer;

internal class BusObserver(ILogger<BusObserver> logger) : IBusObserver
{
    public void CreateFaulted(Exception exception)
    {
        logger.LogTrace($"A new fault has been created. Message:{exception.Message}");
    }

    public void PostCreate(IBus bus)
    {
        logger.LogTrace(
            $"Service bus created.Topology: {bus.Topology} - Service Bus Type: {bus.Address}"
        );
    }

    public Task PostStart(IBus bus, Task<BusReady> busReady)
    {
        logger.LogTrace(
            $"Service bus started.Topology: {bus.Topology} - Service Bus Type: {bus.Address}"
        );

        return Task.CompletedTask;
    }

    public Task PostStop(IBus bus)
    {
        logger.LogTrace(
            $"Service bus Stopped.Topology: {bus.Topology} - Service Bus Type: {bus.Address}"
        );
        return Task.CompletedTask;
    }

    public Task PreStart(IBus bus)
    {
        logger.LogTrace(
            $"Service bus pre start.Topology: {bus.Topology} - Service Bus Type: {bus.Address}"
        );
        return Task.CompletedTask;
    }

    public Task PreStop(IBus bus)
    {
        logger.LogTrace(
            $"Service bus pre stop.Topology: {bus.Topology} - Service Bus Type: {bus.Address}"
        );
        return Task.CompletedTask;
    }

    public Task StartFaulted(IBus bus, Exception exception)
    {
        logger.LogTrace($"Start Faulted with Message:{exception.Message}");
        return Task.CompletedTask;
    }

    public Task StopFaulted(IBus bus, Exception exception)
    {
        logger.LogTrace($"Stop Faulted with Message:{exception.Message}");
        return Task.CompletedTask;
    }
}
