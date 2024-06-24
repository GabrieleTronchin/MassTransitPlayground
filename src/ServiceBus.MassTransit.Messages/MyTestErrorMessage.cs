namespace MassTransit.Playground.Messages
{
    public class MyTestErrorMessage
    {
        public Guid Id { get; }
        public DateTime Time { get; }

        public MyTestErrorMessage()
        {
            Id = Guid.NewGuid();
            Time = DateTime.Now;
        }
    }
}
