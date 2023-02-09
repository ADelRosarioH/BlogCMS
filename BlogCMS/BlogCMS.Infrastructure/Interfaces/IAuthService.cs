using BlogCMS.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Interfaces;

public interface IAuthService
{
    Task<BlogUser?> RegisterNewUser(string userName, string email, string password);
    Task<bool> AreCredentialsValid(string userName, string password);
}