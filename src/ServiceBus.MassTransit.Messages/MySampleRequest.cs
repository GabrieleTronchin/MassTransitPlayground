namespace MassTransit.Playground.Messages;

public record MySampleRequest(Guid id, string status);

public record MySampleRequestAccepted(Guid id, DateTime timestamp);
