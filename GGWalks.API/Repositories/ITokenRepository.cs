using Microsoft.AspNetCore.Identity;

namespace GGWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
