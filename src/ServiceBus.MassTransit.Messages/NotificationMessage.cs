namespace ServiceBus.MassTransit.Messages
{
    public class NotificationMessage
    {
        public Guid Id { get; }
        public string Text { get; }

        public string Title { get; }

        public NotificationMessage(string text, string title)
        {
            Id = Guid.NewGuid();
            Text = text;
            Title = title;
        }
    }
}
