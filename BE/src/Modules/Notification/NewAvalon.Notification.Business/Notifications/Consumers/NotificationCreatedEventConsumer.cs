using MassTransit;
using NewAvalon.Messaging.Contracts.Notifications;
using NewAvalon.Notification.Domain.EntityIdentifiers;
using NewAvalon.Notification.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Business.Notifications.Consumers
{
    public sealed class NotificationCreatedEventConsumer : IConsumer<INotificationCreatedEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationUnitOfWork _unitOfWork;

        public NotificationCreatedEventConsumer(
            INotificationRepository notificationRepository,
            INotificationUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
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
            // Should create Job
            /*            var userDetailsRequest = new UserDetailsRequest()
                        {
                            Id = notification.UserId.Value
                        };

                        var userDetailsResponse = await _userDetailsRequestClient.GetResponse<IUserDetailsResponse>(
                                userDetailsRequest,
                                context.CancellationToken);

                        try
                        {
                            await _sendGridClient.SendEmailAsync(CreateSendGridMessage(userDetailsResponse.Message, notification.Type));

                            notification.Publish(DateTime.UtcNow);
                        }
                        catch (Exception ex)
                        {
                            notification.Fail(DateTime.UtcNow);
                        }*/

            await _unitOfWork.SaveChangesAsync(context.CancellationToken);
        }

        /*        private SendGridMessage CreateSendGridMessage(IUserDetailsResponse userDetails, NotificationType type)
                {
                    var sendGridMessage = new SendGridMessage
                    {
                        From = new EmailAddress(_options.Sender, "NewAvalon"),
                        TemplateId = GetTemplateName(type),
                    };

                    sendGridMessage.AddTo(new EmailAddress(userDetails.Email, userDetails.Username));

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
                }*/
    }
}
