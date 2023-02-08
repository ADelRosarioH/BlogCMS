using BlogCMS.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.WebAPI.Extensions;

public static class IdentityServiceCollection
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BlogCMSDbContext>()
            .AddDefaultTokenProviders();
    }

}