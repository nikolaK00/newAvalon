using MediatR;
using NewAvalon.Abstractions.Messaging;
using NewAvalon.Notification.Boundary.Notifications.Commands.SendEmail;
using NewAvalon.Notification.Business.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Business.Notifications.Commands.SendEmail
{
    internal sealed class SendEmailCommandHandler : ICommandHandler<SendEmailCommand, Unit>
    {
        private readonly IEmailService _emailService;

        public SendEmailCommandHandler(IEmailService emailService) => _emailService = emailService;

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmail(request.SenderEmail, request.RecieverEmail, request.Message, cancellationToken);

            return Unit.Value;
        }
    }
}
