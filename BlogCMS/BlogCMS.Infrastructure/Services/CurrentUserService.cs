using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<BlogUser> _userManager;

    private Guid? _currentUserId;
    public Guid CurrentUserId
    {
        get
        {
            if (_currentUserId is not null)
            {
                return _currentUserId.Value;
            }

            var userIdString = _userManager.GetUserId(_httpContext.HttpContext.User);
            _currentUserId = Guid.Parse(userIdString);

            return _currentUserId.Value;
        }
    }
    
    private BlogUser _currentUser;
    public BlogUser CurrentUser
    {
        get
        {
            if (_currentUser is not null)
            {
                return _currentUser;
            }

            var userName = _httpContext.HttpContext.User.Identity.Name;
            var currentUser = _userManager.FindByNameAsync(userName).Result;
            _currentUser = currentUser;

            return _currentUser;
        }
    }

    public CurrentUserService(IHttpContextAccessor httpContext, UserManager<BlogUser> userManager)
    {
        _httpContext = httpContext;
        _userManager = userManager;
    }

    public async Task<bool> IsInRole(string role)
    {
        return await _userManager.IsInRoleAsync(CurrentUser, role);
    }
}