using MassTransit;
using MassTransit.Playground.Messages;
using MassTransit.Playground.Sender.Observer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MassTransit.Playground.Sender;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        //Register observers
        services.AddTransient<IPublishObserver, PublishObserver>();
        services.AddTransient<IMyBus, MyBus>();
        services
            .AddOptions<ServiceBusOptions>()
            .Bind(configuration.GetSection(nameof(ServiceBusOptions)))
            .ValidateDataAnnotations();

        var serviceProvidder = services.BuildServiceProvider();
        var buOptions =
            serviceProvidder.GetRequiredService<IOptions<ServiceBusOptions>>()?.Value
            ?? throw new ArgumentNullException(nameof(ServiceBusOptions));

        var publishObserver = serviceProvidder.GetRequiredService<IPublishObserver>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            switch (buOptions.Type)
            {
                case ServiceBusType.InMemory:
                    x.UsingInMemory(
                        (context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        }
                    );
                    break;

                case ServiceBusType.AzureBus:

                    x.AddServiceBusMessageScheduler();
                    x.UsingAzureServiceBus(
                        (context, cfg) =>
                        {
                            cfg.Host(buOptions.ConnectionString);
                            cfg.ConfigureEndpoints(context);
                            cfg.ConnectPublishObserver(publishObserver);
                        }
                    );

                    break;
                case ServiceBusType.RabbitMQ:

                    x.AddDelayedMessageScheduler();
                    x.UsingRabbitMq(
                        (context, cfg) =>
                        {
                            cfg.Host(buOptions.ConnectionString);
                            cfg.ConfigureEndpoints(context);
                            cfg.ConnectPublishObserver(publishObserver);
                        }
                    );

                    break;

                default:
                    break;
            }
        });

        return services;
    }
}
