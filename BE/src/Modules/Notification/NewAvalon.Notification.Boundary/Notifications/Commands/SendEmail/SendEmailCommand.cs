using MediatR;
using NewAvalon.Abstractions.Messaging;

namespace NewAvalon.Notification.Boundary.Notifications.Commands.SendEmail
{
    public sealed record SendEmailCommand(string SenderEmail, string RecieverEmail, string Message) : ICommand<Unit>;
}
