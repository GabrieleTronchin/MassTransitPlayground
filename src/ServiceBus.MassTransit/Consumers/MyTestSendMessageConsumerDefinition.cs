﻿namespace MassTransit.Playground.Receivers.Consumers;

/// <summary>
/// sample custom consumer configuration it must be on the same namespace
/// </summary>
public class MyTestSendMessageConsumerDefinition : ConsumerDefinition<MyTestSendMessageConsumer>
{
    public MyTestSendMessageConsumerDefinition()
    {
        // override the default endpoint name, for whatever reason
        EndpointName = "my-test-message";

        // limit the number of messages consumed concurrently
        // this applies to the consumer only, not the endpoint
        ConcurrentMessageLimit = 4;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<MyTestSendMessageConsumer> consumerConfigurator
    )
    {
        endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
        endpointConfigurator.PublishFaults = false; // avoid publish fault
    }
}
