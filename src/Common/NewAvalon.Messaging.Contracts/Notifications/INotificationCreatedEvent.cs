﻿using NewAvalon.Abstractions.Messaging;
using NewAvalon.Domain.Enums;
using System;
using System.Collections.Generic;

namespace NewAvalon.Messaging.Contracts.Notifications
{
    public interface INotificationCreatedEvent : IEvent
    {
        public Guid? UserId { get; set; }

        public string Email { get; set; }

        public DeliveryMechanism DeliveryMechanism { get; set; }

        public NotificationType NotificationType { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public Dictionary<string, object> Details { get; set; }
    }
}
