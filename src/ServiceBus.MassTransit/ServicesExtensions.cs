using MassTransit.Playground.Messages;
using MassTransit.Playground.Receivers.Consumers;
using MassTransit.Playground.Receivers.Observers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MassTransit.Playground.Receivers;

public static partial class ServicesExtensions
{
    public static IServiceCollection AddServiceBus(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services
            .AddOptions<ServiceBusOptions>()
            .Bind(configuration.GetSection(nameof(ServiceBusOptions)))
            .ValidateDataAnnotations();

        //Register observers
        services.AddTransient<IConsumeObserver, GlobalConsumeObserver>();

        var serviceProvidder = services.BuildServiceProvider();
        var buOptions =
            serviceProvidder.GetRequiredService<IOptions<ServiceBusOptions>>()?.Value
            ?? throw new ArgumentNullException(nameof(ServiceBusOptions));

        var globalConsumerObserver = serviceProvidder.GetRequiredService<IConsumeObserver>();

        services.AddMassTransit(x =>
        {
            x.AddConsumersFromNamespaceContaining<MailNotificationConsumer>();

            x.SetKebabCaseEndpointNameFormatter();

            switch (buOptions.Type)
            {
                case ServiceBusType.InMemory:
                    x.UsingInMemory(
                        (context, cfg) =>
                        {
                            cfg.UseScheduledRedelivery(r =>
                                r.Intervals(
                                    TimeSpan.FromMinutes(5),
                                    TimeSpan.FromMinutes(15),
                                    TimeSpan.FromMinutes(30)
                                )
                            );
                            cfg.UseMessageRetry(r => r.Immediate(5));
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
                            cfg.UseScheduledRedelivery(r =>
                                r.Intervals(
                                    TimeSpan.FromMinutes(5),
                                    TimeSpan.FromMinutes(15),
                                    TimeSpan.FromMinutes(30)
                                )
                            );
                            cfg.UseMessageRetry(r => r.Immediate(5));
                            cfg.ConfigureEndpoints(context);
                            cfg.ConnectConsumeObserver(globalConsumerObserver);
                        }
                    );

                    break;
                case ServiceBusType.RabbitMQ:
                    x.AddDelayedMessageScheduler();

                    x.UsingRabbitMq(
                        (context, cfg) =>
                        {
                            cfg.Host(buOptions.ConnectionString);
                            cfg.UseScheduledRedelivery(r =>
                                r.Intervals(
                                    TimeSpan.FromMinutes(5),
                                    TimeSpan.FromMinutes(15),
                                    TimeSpan.FromMinutes(30)
                                )
                            );
                            cfg.UseMessageRetry(r => r.Immediate(5));
                            cfg.ConfigureEndpoints(context);
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
