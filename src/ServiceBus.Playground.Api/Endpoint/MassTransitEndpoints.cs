using MassTransit.Playground.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Playground.Sender.Api.Endpoint;

public class MassTransitEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/SendOnQueue/SendMessage",
                ([FromServices] IMyBus bus) =>
                {
                    return bus.Send("queue:my-test-message", new MyTestSendMessage());
                }
            )
            .WithName("SendMessage")
            .WithOpenApi();

        app.MapPost(
                "/SendBatch/SendBatchMessage",
                ([FromServices] IMyBus bus) =>
                {
                    var lstMessages = new Array[100].Select(x => new MyTestSendMessage());

                    return bus.SendBatch("queue:my-test-message", lstMessages);
                }
            )
            .WithName("SendBatchMessage")
            .WithOpenApi();

        app.MapPost(
                "/Publish/NotificationMessage",
                ([FromServices] IMyBus bus) =>
                {
                    return bus.Publish(new NotificationMessage("My Test Message", "Test Title"));
                }
            )
            .WithName("NotificationMessage")
            .WithOpenApi();

        app.MapPost(
                "/PublishWithError/MyTestMessage",
                ([FromServices] IMyBus bus) =>
                {
                    return bus.Publish(new MyTestErrorMessage());
                }
            )
            .WithName("MyTestMessage")
            .WithOpenApi();

        app.MapPost(
                "/PublishBatch/MyTestBatchMessage",
                ([FromServices] IMyBus bus) =>
                {
                    var lstMessages = new Array[100].Select(x => new MyTestBatchMessage());

                    return bus.PublishBatch(lstMessages);
                }
            )
            .WithName("MyTestBatchMessage")
            .WithOpenApi();

        app.MapPost(
                "/PublishDelay/DelayMessage",
                ([FromServices] IMyBus bus) =>
                {
                    var lstMessages = new Array[100].Select(x => new MyTestBatchMessage());

                    return bus.Publish(new MyTestErrorMessage(), TimeSpan.FromSeconds(5));
                }
            )
            .WithName("DelayMessage")
            .WithOpenApi();
    }
}
