namespace MassTransit.Playground.Sender.Api.Endpoint;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
