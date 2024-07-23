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
        services.AddTransient<ISendObserver, SendObserver>();
        services.AddTransient<IBusObserver, BusObserver>();

        services.AddTransient<IMyBus, MyBus>();
        services
            .AddOptions<ServiceBusOptions>()
            .Bind(configuration.GetSection(nameof(ServiceBusOptions)))
            .ValidateDataAnnotations();

        var serviceProvider = services.BuildServiceProvider();

        var buOptions =
            serviceProvider.GetRequiredService<IOptions<ServiceBusOptions>>()?.Value
            ?? throw new ArgumentNullException(nameof(ServiceBusOptions));

        var publishObserver = serviceProvider.GetRequiredService<IPublishObserver>();
        var sendObserver = serviceProvider.GetRequiredService<ISendObserver>();
        var busObserver = serviceProvider.GetRequiredService<IBusObserver>();

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            switch (buOptions.Type)
            {
                case ServiceBusType.InMemory:
                    x.AddDelayedMessageScheduler();
                    x.UsingInMemory(
                        (context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                            cfg.ConnectPublishObserver(publishObserver);
                            cfg.ConnectSendObserver(sendObserver);
                            cfg.ConnectBusObserver(busObserver);
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
                            cfg.ConnectSendObserver(sendObserver);
                            cfg.ConnectBusObserver(busObserver);
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
                            cfg.ConnectSendObserver(sendObserver);
                            cfg.ConnectBusObserver(busObserver);
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
