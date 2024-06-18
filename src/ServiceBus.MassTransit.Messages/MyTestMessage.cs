namespace ServiceBus.MassTransit.Messages
{
    public class MyTestMessage
    {
        public Guid Id { get; }
        public DateTime Time { get; }

        public MyTestMessage()
        {
            Id = Guid.NewGuid();
            Time = DateTime.Now;
        }
    }
}
