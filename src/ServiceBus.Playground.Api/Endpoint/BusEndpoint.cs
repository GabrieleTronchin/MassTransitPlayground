using MassTransit.Playground.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Playground.Sender.Api.Endpoint;

public class BusEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/Bus/Publish/NotificationMessage",
                async ([FromServices] IBus bus) =>
                {
                    return bus.Publish(new NotificationMessage("My Test Message", "Test Title"));
                }
            )
            .WithName("BusNotificationMessage")
            .WithOpenApi();
    }
}
