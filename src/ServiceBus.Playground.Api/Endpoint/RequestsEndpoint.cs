using MassTransit.Playground.Messages;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Playground.Sender.Api.Endpoint;

public class RequestsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "/Request/SampleRequest",
                async ([FromServices] IRequestClient<MySampleRequest> bus) =>
                {
                    Response<MySampleRequestAccepted> response =
                        await bus.GetResponse<MySampleRequestAccepted>(
                            new MySampleRequest(Guid.NewGuid(), "New")
                        );
                    return response;
                }
            )
            .WithName("SampleRequest")
            .WithOpenApi();
    }
}
