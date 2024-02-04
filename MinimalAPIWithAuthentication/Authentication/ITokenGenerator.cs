using MinimalAPIWithAuthentication.Entities;

namespace MinimalAPIWithAuthentication.Authentication
{
    public interface ITokenGenerator
    {
        string GenerateToken(User user);
    }
}