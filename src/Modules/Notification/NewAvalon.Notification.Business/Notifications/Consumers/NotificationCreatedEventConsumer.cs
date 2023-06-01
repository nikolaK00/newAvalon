using MassTransit;
using Microsoft.Extensions.Options;
using NewAvalon.Domain.Enums;
using NewAvalon.Messaging.Contracts.Notifications;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Notification.Business.Options;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using NewAvalon.Notification.Domain.Repositories;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Business.Notifications.Consumers
{
    public sealed class NotificationCreatedEventConsumer : IConsumer<INotificationCreatedEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IRequestClient<IUserDetailsRequest> _userDetailsRequestClient;
        private readonly EmailNotificationsJobOptions _options;
        private readonly EmailTemplatesOptions _emailTemplatesOptions;
        private readonly SendGridClient _sendGridClient;
        private readonly INotificationUnitOfWork _unitOfWork;

        public NotificationCreatedEventConsumer(
            INotificationRepository notificationRepository,
            IRequestClient<IUserDetailsRequest> userDetailsRequestClient,
            IOptions<EmailNotificationsJobOptions> options,
            IOptions<EmailTemplatesOptions> emailTemplateOptions,
            SendGridClient sendGridClient,
            INotificationUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _userDetailsRequestClient = userDetailsRequestClient;
            _options = options.Value;
            _sendGridClient = sendGridClient;
            _emailTemplatesOptions = emailTemplateOptions.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<INotificationCreatedEvent> context)
        {
            INotificationCreatedEvent notificationCreated = context.Message;

            Domain.Entities.Notification notification = notificationCreated.UserId.HasValue
                ? new Domain.Entities.Notification(
                    new NotificationId(Guid.NewGuid()),
                    notificationCreated.UserId.Value,
                    notificationCreated.DeliveryMechanism,
                    notificationCreated.NotificationType,
                    notificationCreated.Title,
                    notificationCreated.Content,
                    notificationCreated.Details)
                : new Domain.Entities.Notification(
                    new NotificationId(Guid.NewGuid()),
                    notificationCreated.Email,
                    notificationCreated.DeliveryMechanism,
                    notificationCreated.NotificationType,
                    notificationCreated.Title,
                    notificationCreated.Content,
                    notificationCreated.Details);

            _notificationRepository.Insert(notification);

            /*            var userDetailsRequest = new UserDetailsRequest()
                        {
                            Id = notification.UserId.Value
                        };

                        var userDetails =
                            await _userDetailsRequestClient.GetResponse<IUserDetailsResponse>(userDetailsRequest,
                                context.CancellationToken);

                        await _sendGridClient.SendEmailAsync(CreateSendGridMessage(userDetails.Message, notification.Type));*/

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }

        private SendGridMessage CreateSendGridMessage(IUserDetailsResponse userDetails, NotificationType type)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress("knez.nikola00@outlook.com", "New Avalon"),
                TemplateId = GetTemplateName(type)
            };

            sendGridMessage.AddTo(userDetails.Email, userDetails.UserName);

            return sendGridMessage;
        }

        private string GetTemplateName(NotificationType type) =>
            type switch
            {
                NotificationType.DealerUserApproved => _emailTemplatesOptions.UserVerifiedTemplate,
                _ => _emailTemplatesOptions.UserVerifiedTemplate,
            };

        private sealed class UserDetailsRequest : IUserDetailsRequest
        {
            public Guid Id { get; set; }
        }
    }
}
