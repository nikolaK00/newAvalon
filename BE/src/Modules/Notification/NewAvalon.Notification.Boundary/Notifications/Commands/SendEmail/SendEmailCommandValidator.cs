using FluentValidation;

namespace NewAvalon.Notification.Boundary.Notifications.Commands.SendEmail
{
    public sealed class SendEmailToTechSupportCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailToTechSupportCommandValidator()
        {
            RuleFor(x => x.SenderEmail).EmailAddress();

            RuleFor(x => x.RecieverEmail).EmailAddress();

            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
