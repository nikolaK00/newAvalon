using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;

namespace Plato.Domain.Exceptions.Images
{
    public sealed class ImageNotFoundException : NotFoundException
    {
        public ImageNotFoundException(Guid imageId)
            : base(
                "Image not found",
                $"The image with the identifier {imageId} was not found.",
                Error.ImageNotFound)
        {
        }
    }
}
