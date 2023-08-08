using NewAvalon.Abstractions.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Abstractions.Services
{
    public interface IImageService
    {
        Task<ImageResponse> GetByIdAsync(Guid imageId, CancellationToken cancellationToken);
    }
}