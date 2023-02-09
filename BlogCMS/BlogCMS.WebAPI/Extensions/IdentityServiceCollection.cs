using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogCMS.WebAPI.Extensions;

public static class IdentityServiceCollection
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<BlogUser, IdentityRole<Guid>>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<BlogCMSDbContext>()
            .AddDefaultTokenProviders();
    }

}