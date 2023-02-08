using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public AuthService(UserManager<IdentityUser<Guid>> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityUser<Guid>?> RegisterNewUser(string userName, string email, string password)
    {
        var result = await _userManager.CreateAsync(new IdentityUser<Guid>()
        {
            UserName = userName,
            Email = email,
        }, password);

        return result == IdentityResult.Success
            ? await _userManager.FindByNameAsync(userName)
            : null;
    }
    
    public async Task<bool> AreCredentialsValid(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user is not null && await _userManager.CheckPasswordAsync(user, password);
    }
}