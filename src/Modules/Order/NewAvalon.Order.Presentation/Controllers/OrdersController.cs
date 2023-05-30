using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Authorization;
using NewAvalon.Authorization.Attributes;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Order.Boundary.Orders.Queries.GetAllOrders;
using NewAvalon.Order.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Order.Presentation.Controllers
{
    /// <summary>
    /// Represents the orders controller.
    /// </summary>
    public sealed class OrdersController : OrderController
    {
        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet]
        [Authorize]
        [HasPermission(Permissions.OrderRead)]
        [ProducesResponseType(typeof(PagedList<OrderDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPendingUsers(
            int page,
            int itemsPerPage,
            CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(HttpContext.User.GetUserIdentityId());

            var query = new GetAllOrdersQuery(
                userId,
                page,
                itemsPerPage);

            PagedList<OrderDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }
    }
}
