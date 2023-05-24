namespace NewAvalon.Notification.Infrastructure.Options
{
    public class EmailNotificationsJobOptions
    {
        public int BatchSize { get; set; }

        public string ApiKey { get; set; }

        public int IntervalInSeconds { get; set; }
    }
}
