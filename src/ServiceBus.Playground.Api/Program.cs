using ServiceBus.Playground.Sender;
using ServiceBus.Playground.Sender.Api.Endpoint;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceBus(builder.Configuration);

builder.Services.AddEndpoints([typeof(MassTransitEndpoints).Assembly, typeof(IEndpoint).Assembly]);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
