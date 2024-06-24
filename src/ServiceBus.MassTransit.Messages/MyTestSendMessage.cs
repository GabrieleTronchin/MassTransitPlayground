namespace MassTransit.Playground.Messages;

public class MyTestSendMessage
{
    public Guid Id { get; }
    public DateTime Time { get; }

    public MyTestSendMessage()
    {
        Id = Guid.NewGuid();
        Time = DateTime.Now;
    }
}
