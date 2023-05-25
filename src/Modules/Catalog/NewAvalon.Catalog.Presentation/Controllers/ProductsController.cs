using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Catalog.Presentation.Controllers
{
    /// <summary>
    /// Represents the dealers controller.
    /// </summary>
    [Route("api/catalog/product")]
    public sealed class ProductsController : CatalogController
    {
        /// <summary>
        /// Creates a new product based on the specified request.
        /// </summary>
        /// <param name="request">The create product request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The identifier of the newly created product.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateUser([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>();

            EntityCreatedResponse response = await Sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetProduct), new { productId = response.EntityId }, response.EntityId);
        }

        /// <summary>
        /// Gets the user with the specified identifier.
        /// </summary>
        /// <param name="productId">The user identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpGet("{productId:guid}", Name = nameof(GetProduct))]
        [ProducesResponseType(typeof(ProductDetailsResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct(Guid productId, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(productId);

            ProductDetailsResponse response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }
    }
}
