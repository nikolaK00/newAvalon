using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Abstractions;
using NewAvalon.Domain.Enums;
using NewAvalon.UserAdministration.Domain.Entities;
using NewAvalon.UserAdministration.Domain.Events;
using NewAvalon.UserAdministration.Domain.Exceptions.Users;
using NewAvalon.UserAdministration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Business.Users.Events.UserCreated
{
    public class UserCreatedNotificationDomainEvent : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private const string Title = "User successfully created";

        private readonly IUserRepository _userRepository;
        private readonly IEventPublisher _publisher;

        public UserCreatedNotificationDomainEvent(
            IUserRepository userRepository,
            IEventPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(notification.UserId.Value);
            }

            string title = Title;

            string content = $"{user.FirstName} {user.LastName} has been successfully created.";

            var details = new Dictionary<string, object>
            {
                { "userId", user.Id.Value },
            };

            await SendNotification(
                null,
                user.Email,
                DeliveryMechanism.Email,
                NotificationType.UserCreated,
                title,
                content,
                details,
                cancellationToken);
        }

        private async Task SendNotification(
            Guid? userId,
            string email,
            DeliveryMechanism deliveryMechanism,
            NotificationType notificationType,
            string title,
            string content,
            Dictionary<string, object> details,
            CancellationToken cancellationToken)
        {
            var notification = new NotificationCreatedEvent
            {
                UserId = userId,
                Email = email,
                Title = title,
                Content = content,
                DeliveryMechanism = deliveryMechanism,
                NotificationType = notificationType,
                Details = details
            };

            await _publisher.PublishAsync(notification, cancellationToken);
        }
    }
}
