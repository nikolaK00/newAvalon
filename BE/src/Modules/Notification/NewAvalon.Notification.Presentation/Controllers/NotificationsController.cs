using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Notification.Boundary.Notifications.Commands.SendEmail;
using NewAvalon.Notification.Presentation.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Notification.Presentation.Controllers
{
    /// <summary>
    /// Represents the base Notification controller.
    /// </summary>
    public sealed class NotificationsController : NotificationModuleController
    {
        /// <summary>
        /// Sends email.
        /// </summary>
        /// <param name="message">Message for tech support.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identifier of the newly created line item.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<SendEmailCommand>();

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
