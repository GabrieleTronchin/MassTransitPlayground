using Microsoft.AspNetCore.Mvc;
using ServiceBus.MassTransit.Messages;

namespace MassTransit.Playground.Sender.Api.Endpoint;

public class MassTransitEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/MassTransit/NotificationMessage",
                ([FromServices] IMyBus bus) =>
                {
                    return bus.Publish(new NotificationMessage("My Test Message", "Test Title"));
                }
            )
            .WithName("NotificationMessage")
            .WithOpenApi();

        app.MapPost(
                "/MassTransit/MyTestMessage",
                ([FromServices] IMyBus bus) =>
                {
                    return bus.Publish(new MyTestMessage());
                }
            )
            .WithName("MyTestMessage")
            .WithOpenApi();



        app.MapPost(
               "/MassTransit/MyTestBatchMessage",
               ([FromServices] IMyBus bus) =>
               {
                   var lstMessages = new Array[100].Select(x => new MyTestBatchMessage());

                   return bus.PublishBatch(lstMessages);
               }
           )
           .WithName("MyTestBatchMessage")
           .WithOpenApi();




        app.MapPost(
               "/MassTransit/DelayMessage",
               ([FromServices] IMyBus bus) =>
               {
                   var lstMessages = new Array[100].Select(x => new MyTestBatchMessage());

                   return bus.Publish(new MyTestMessage(), TimeSpan.FromSeconds(5));
               }
           )
           .WithName("DelayMessage")
           .WithOpenApi();

    }
}
