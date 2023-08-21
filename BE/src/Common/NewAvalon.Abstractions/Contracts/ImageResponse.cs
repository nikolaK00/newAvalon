using System;

namespace NewAvalon.Abstractions.Contracts
{
    public sealed record ImageResponse(bool Exists, Guid ImageId, string ImageUrl);
}
