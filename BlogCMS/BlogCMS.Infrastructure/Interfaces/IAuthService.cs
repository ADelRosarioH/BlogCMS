using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<IdentityUser<Guid>?> RegisterNewUser(string userName, string email, string password);
    Task<bool> AreCredentialsValid(string userName, string password);
}