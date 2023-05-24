using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Boundary.Pagination;
using NewAvalon.UserAdministration.Boundary.Users.Commands.ApproveUser;
using NewAvalon.UserAdministration.Boundary.Users.Commands.CreateUser;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUnApprovedUsers;
using NewAvalon.UserAdministration.Boundary.Users.Queries.GetUser;
using NewAvalon.UserAdministration.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.UserAdministration.Presentation.Controllers
{
    /// <summary>
    /// Represents the User controller.
    /// </summary>
    public sealed class UsersController : UserAdministrationController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        public UsersController() { }

        /// <summary>
        /// Creates a new user based on the specified request.
        /// </summary>
        /// <param name="request">The create user request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identifier of the newly created user.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            CreateUserCommand command = request.Adapt<CreateUserCommand>();

            EntityCreatedResponse response = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetUser), new { userId = response.EntityId }, response.EntityId);
        }

        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("{userId:guid}", Name = nameof(GetUser))]
        [ProducesResponseType(typeof(UserDetailsResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(userId);

            UserDetailsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets the users that are not approved.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("un-approved")]
        [ProducesResponseType(typeof(PagedList<UserDetailsResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUnApprovedUsers(
            int page,
            int itemsPerPage,
            CancellationToken cancellationToken)
        {
            var query = new GetUnApprovedUsersQuery(page, itemsPerPage);

            PagedList<UserDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Approve user with the specified identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpPut("approve/{userId:guid}", Name = nameof(GetUser))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveUser(Guid userId, CancellationToken cancellationToken)
        {
            var command = new ApproveUserByIdCommand(userId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
