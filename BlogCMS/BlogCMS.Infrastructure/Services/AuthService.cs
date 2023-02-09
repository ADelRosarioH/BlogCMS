using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
using BlogCMS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<BlogUser> _userManager;

    public AuthService(UserManager<BlogUser> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<BlogUser?> RegisterNewUser(string userName, string email, string role, string password)
    {
        var result = await _userManager.CreateAsync(new BlogUser
        {
            UserName = userName,
            Email = email,
        }, password);

        var newUser = await _userManager.FindByNameAsync(userName);

        if (!new[] {Roles.Writer, Roles.Editor, Roles.Public}
                .Select(r => r.ToLower())
                .Contains(role.ToLower()))
        {
            throw new Exception("Invalid Role. Role must be Writer, Editor or Public.");
        }
        
        await _userManager.AddToRoleAsync(newUser, role);
        
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