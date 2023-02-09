using BlogCMS.Infrastructure.Helpers.Constants;
using BlogCMS.WebAPI.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BlogCMS.WebAPI.Extensions;

public static class AuthorizationServiceCollection
{
    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(opts =>
        {
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            
            opts.AddPolicy(Policies.CanCreateNewPostPolicy, Handlers.CanCreateNewPostPolicyHandler);
            opts.AddPolicy(Policies.CanUpdatePostPolicy, Handlers.CanUpdatePostPolicyHandler);
            opts.AddPolicy(Policies.CanApprovePostPolicy, Handlers.CanApprovePostPolicyHandler);
            opts.AddPolicy(Policies.CanRejectPostPolicy, Handlers.CanRejectPostPolicyHandler);
        });
        
        return services;
    }
}