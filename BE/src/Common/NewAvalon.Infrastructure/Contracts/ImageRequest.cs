using NewAvalon.Messaging.Contracts.Images;
using System;

namespace NewAvalon.Infrastructure.Contracts
{
    internal sealed class ImageRequest : IImageRequest
    {
        public Guid ImageId { get; set; }
    }
}
