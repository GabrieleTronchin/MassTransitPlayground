using System.ComponentModel.DataAnnotations;

namespace MassTransit.Playground.Messages
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
