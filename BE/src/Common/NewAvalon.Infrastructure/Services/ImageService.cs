using Mapster;
using MassTransit;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Abstractions.Services;
using NewAvalon.Infrastructure.Contracts;
using NewAvalon.Messaging.Contracts.Images;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Infrastructure.Services
{
    internal sealed class ImageService : IImageService, ITransient
    {
        private readonly IRequestClient<IImageRequest> _imageRequestClient;

        public ImageService(IRequestClient<IImageRequest> imageRequestClient) => _imageRequestClient = imageRequestClient;

        public async Task<ImageResponse> GetByIdAsync(Guid imageId, CancellationToken cancellationToken)
        {
            var request = new ImageRequest
            {
                ImageId = imageId
            };

            var response = (await _imageRequestClient.GetResponse<IImageResponse>(request, cancellationToken)).Message;

            return response.Adapt<ImageResponse>();
        }
    }
}
