/*using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NewAvalon.Domain.Enums;
using NewAvalon.Messaging.Contracts.Users;
using NewAvalon.Notification.Infrastructure.Options;
using NewAvalon.Notification.Persistence;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Infrastructure.BackgroundTasks
{
    [DisallowConcurrentExecution]
    public sealed class SendEmailNotificationsJob : IJob
    {
        private readonly NotificationDbContext _dbContext;
        private readonly IRequestClient<IUserDetailsListRequest> _userDetailsListRequestClient;
        private readonly IMandrillApi _mandrillApi;
        private readonly ILogger<SendEmailNotificationsJob> _logger;
        private readonly EmailNotificationsJobOptions _options;
        private readonly EmailTemplatesOptions _emailTemplatesOptions;

        public SendEmailNotificationsJob(
            NotificationDbContext dbContext,
            IRequestClient<IUserDetailsListRequest> userDetailsListRequestClient,
            IMandrillApi mandrillApi,
            ILogger<SendEmailNotificationsJob> logger,
            IOptions<EmailNotificationsJobOptions> options,
            IOptions<EmailTemplatesOptions> emailTemplatesOptions)
        {
            _dbContext = dbContext;
            _userDetailsListRequestClient = userDetailsListRequestClient;
            _mandrillApi = mandrillApi;
            _logger = logger;
            _options = options.Value;
            _emailTemplatesOptions = emailTemplatesOptions.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Starting {@JobName} job.", nameof(SendEmailNotificationsJob));

            List<Domain.Entities.Notification> notifications = await _dbContext.Set<Domain.Entities.Notification>()
                .Where(notification =>
                    !notification.Published &&
                    !notification.Failed &&
                    notification.DeliveryMechanism == DeliveryMechanism.Email)
                .OrderBy(notification => notification.CreatedOnUtc)
                .Take(_options.BatchSize)
                .ToListAsync(context.CancellationToken);

            if (!notifications.Any())
            {
                return;
            }

            var userDetailsListRequest = new UserDetailsListRequest
            {
                UserIds = notifications
                    .Where(n => n.UserId.HasValue)
                    .Select(n => n.UserId.Value)
                    .Distinct()
                    .ToArray()
            };

            IUserDetailsListResponse userDetailsListResponse =
                (await _userDetailsListRequestClient.GetResponse<IUserDetailsListResponse>(
                    userDetailsListRequest,
                    context.CancellationToken))
                .Message;

            foreach (Domain.Entities.Notification notification in notifications)
            {
                try
                {
                    string email;

                    if (notification.UserId.HasValue)
                    {
                        IUserDetailsResponse userDetails = userDetailsListResponse.Users.First(u => u.Id == notification.UserId.Value);

                        email = userDetails.Email;
                    }
                    else
                    {
                        email = notification.Email;
                    }

                    var message = new MandrillMessage(_options.Sender, email, notification.Title, notification.Content)
                    {
                        MergeLanguage = MandrillMessageMergeLanguage.Mailchimp,
                        GlobalMergeVars = new List<MandrillMergeVar>
                        {
                            new()
                            {
                                Name = "TITLE",
                                Content = notification.Title
                            },
                            new()
                            {
                                Name = "EMAIL_BODY",
                                Content = notification.Content
                            }
                        }
                    };

                    var detailsParser = new NotificationDetailsParser();

                    message.GlobalMergeVars.AddRange(detailsParser.Parse(notification.Type, notification.Details));

                    IList<MandrillSendMessageResponse> messageSendResponses = await _mandrillApi
                        .Messages
                        .SendTemplateAsync(
                            message,
                            GetTemplateName(notification.Type));

                    if (messageSendResponses.Any(r => r.Status == MandrillSendMessageResponseStatus.Invalid || r.Status == MandrillSendMessageResponseStatus.Rejected))
                    {
                        MandrillSendMessageResponse response = messageSendResponses.First();

                        throw new Exception($"Email notification sending failed. Status - '{response.Status}'. Reason - '{response.RejectReason}'");
                    }

                    notification.Publish(DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed sending an email notification '{notification.Id.Value}' for user '{notification.UserId}'", ex);

                    notification.Fail(DateTime.UtcNow);
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        private string GetTemplateName(NotificationType type) =>
            type switch
            {
                NotificationType.DealerUserApproved => _emailTemplatesOptions.AcknowledgementTemplate,
                _ => _emailTemplatesOptions.OrderTemplate,
            };

        private sealed class UserDetailsListRequest : IUserDetailsListRequest
        {
            public Guid[] UserIds { get; set; }
        }
    }
}
*/