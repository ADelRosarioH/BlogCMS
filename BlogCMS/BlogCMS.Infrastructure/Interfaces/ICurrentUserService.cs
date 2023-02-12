using BlogCMS.Infrastructure.Entities;

namespace BlogCMS.Infrastructure.Interfaces;

public interface ICurrentUserService
{
    Guid CurrentUserId { get; }
    BlogUser CurrentUser { get; }
    Task<bool> IsInRole(string role);
}