using BlogCMS.Infrastructure.Helpers;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Services;

namespace BlogCMS.WebAPI.Extensions;

public static class BlogCMSServiceCollection
{
    public static IServiceCollection AddBlogCMSServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}