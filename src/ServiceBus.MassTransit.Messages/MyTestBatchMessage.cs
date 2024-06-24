namespace MassTransit.Playground.Messages;

public class MyTestBatchMessage
{
    public Guid Id { get; }
    public DateTime Time { get; }

    public MyTestBatchMessage()
    {
        Id = Guid.NewGuid();
        Time = DateTime.Now;
    }
}
