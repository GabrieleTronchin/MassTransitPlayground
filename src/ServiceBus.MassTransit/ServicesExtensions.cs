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

        // Register the observer as a scoped dependency
        services.AddScoped<IConsumeMessageObserver<MySampleRequestConsumer>, MySampleRequestConsumerObserver>();

        services.AddTransient<IRetryObserver, RetryObserver>();

        var serviceProvider = services.BuildServiceProvider();
        var buOptions =
            serviceProvider.GetRequiredService<IOptions<ServiceBusOptions>>()?.Value
            ?? throw new ArgumentNullException(nameof(ServiceBusOptions));

        var globalConsumerObserver = serviceProvider.GetRequiredService<IConsumeObserver>();
        var retryObserver = serviceProvider.GetRequiredService<IRetryObserver>();
        var messageObserver = serviceProvider.GetRequiredService<IConsumeMessageObserver<MySampleRequestConsumer>>();


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
                            cfg.UseMessageRetry(r =>
                            {
                                r.Immediate(2);
                                r.ConnectRetryObserver(retryObserver);
                            });

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
                                    TimeSpan.FromSeconds(1),
                                    TimeSpan.FromSeconds(2),
                                    TimeSpan.FromSeconds(3)
                                )
                            );
                            cfg.UseMessageRetry(r =>
                            {
                                r.Immediate(2);
                                r.ConnectRetryObserver(retryObserver);
                            });
                            cfg.ConfigureEndpoints(context);
                            cfg.ConnectConsumeObserver(globalConsumerObserver);
                            //cfg.ConnectConsumeMessageObserver(globalConsumerObserver);
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
                                    TimeSpan.FromSeconds(1),
                                    TimeSpan.FromSeconds(2),
                                    TimeSpan.FromSeconds(3)
                                )
                            );
                            cfg.UseMessageRetry(r =>
                            {
                                r.Immediate(2);
                                r.ConnectRetryObserver(retryObserver);
                            });
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
