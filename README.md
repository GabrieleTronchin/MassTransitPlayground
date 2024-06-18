# MassTransit Playground

This repository serves as a playground for testing various features of MassTransit, an open-source framework for message-based distributed applications.

Explore MassTransit [here](https://masstransit.io/).

## Source Code Explanation

This project consists of two main services:

1. **MassTransit.Playground.Sender.Api**: Exposes various methods to send messages to a bus using MassTransit.
2. **MassTransit.Playground.Receiver.Api**: Does not expose any APIs; it simply consumes messages from a service bus.

[!image](./assets/swagger.png)

The project demonstrates switching between Azure Service Bus and RabbitMQ using MassTransit. To switch the service bus provider, modify the `ServiceBusOptions` property in the `appsettings.json` file:

```json
"ServiceBusOptions": {
  "Type": "AzureBus",
  "ConnectionString": ""
}
```

A Docker Compose file is available in the `docker` folder to set up RabbitMQ.

## Topics Covered

This project covers several key topics related to MassTransit:

- Publishing a message
- Publishing a message with a delay
- Publishing a batch of messages
- Receiving a message
- Receiving the same message on different subscribers
- Receiving messages in batches
- Using observers to intercept published and subscribed events
- Implementing simple retry strategies for message errors

This repository is a comprehensive resource for learning and experimenting with MassTransit features in a practical, hands-on manner.