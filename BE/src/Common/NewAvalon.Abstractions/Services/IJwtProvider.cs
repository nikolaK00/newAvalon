using NewAvalon.Abstractions.Contracts;

namespace NewAvalon.Abstractions.Services
{
    public interface IJwtProvider
    {
        string Generate(GenerateTokenRequest user);
    }
}
