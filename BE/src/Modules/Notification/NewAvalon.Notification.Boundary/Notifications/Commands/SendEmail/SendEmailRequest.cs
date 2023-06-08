namespace NewAvalon.Notification.Boundary.Notifications.Commands.SendEmail
{
    public sealed record SendEmailRequest(string SenderEmail, string RecieverEmail, string Message);
}
