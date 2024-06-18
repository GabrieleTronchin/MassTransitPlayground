using System.ComponentModel.DataAnnotations;

namespace ServiceBus.MassTransit.Messages
{
    public class ServiceBusOptions
    {
        [Required]
        public required ServiceBusType Type { get; set; }

        [Required]
        public required string ConnectionString { get; set; }
    }

    public enum ServiceBusType
    {
        InMemory,
        AzureBus,
        RabbitMQ
    }
}
