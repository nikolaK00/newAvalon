using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Authorization.Extensions;
using NewAvalon.Boundary.Pagination;
using NewAvalon.Catalog.Boundary.Products.Commands.CreateProduct;
using NewAvalon.Catalog.Boundary.Products.Commands.DeleteProduct;
using NewAvalon.Catalog.Boundary.Products.Commands.UpdateProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProduct;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProducts;
using NewAvalon.Catalog.Boundary.Products.Queries.GetProductsByCreator;
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
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            CreateProductCommand command = request.Adapt<CreateProductCommand>() with
            {
                CreatorId = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

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

        /// <summary>
        /// Gets all product.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="creatorId">The creator identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpGet("creator")]
        [ProducesResponseType(typeof(PagedList<ProductDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByCreator(Guid creatorId, int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            var query = new GetProductsByCreatorQuery(creatorId, page, itemsPerPage);

            PagedList<ProductDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Gets all product by creator identifier.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<ProductDetailsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts(int page, int itemsPerPage, CancellationToken cancellationToken)
        {
            var query = new GetProductsQuery(page, itemsPerPage);

            PagedList<ProductDetailsResponse> response = await Sender.Send(query, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Update product with specified identifier.
        /// </summary>
        /// <param name="request">Update product request</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of products.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateProductCommand>() with
            {
                CreatorId = Guid.Parse(HttpContext.User.GetUserIdentityId())
            };

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Delete the product with the specified identifier.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user with the specified identifier.</returns>
        [HttpDelete("{productId:guid}", Name = nameof(GetProduct))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromBody] Guid productId, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(productId, Guid.Parse(HttpContext.User.GetUserIdentityId()));

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
