using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
using BlogCMS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Identity.Web;

namespace BlogCMS.WebAPI.Authorization;

public static class Handlers
{
    public static void CanCreateNewPostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(context => context.User.IsInRole(Roles.Writer));
    }

    public static void CanUpdatePostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(async context =>
        {
            var postService = (context.Resource as DefaultHttpContext)?
                .HttpContext?
                .RequestServices?
                .GetService<IPostService>();
            
            var userManager = (context.Resource as DefaultHttpContext)?
                .HttpContext?
                .RequestServices?
                .GetService<UserManager<BlogUser>>();

            var currentUser = await userManager.FindByNameAsync(context.User.Identity.Name);
            
            var postIdString = (context.Resource as DefaultHttpContext)?
                               .HttpContext?
                               .Request?
                               .RouteValues["postId"] as string ?? string.Empty;

            var postId = Guid.Parse(postIdString);
            var currentPost = await postService.GetPostById(postId);

            // Writer can only update his own posts when status is draft or rejected
            return await userManager.IsInRoleAsync(currentUser, Roles.Writer) &&
                   await postService.IsPostAuthor(currentUser.Id, postId) &&
                   (currentPost.Status == PostStatus.Draft || currentPost.Status == PostStatus.Rejected);
        });
    }

    public static void CanApprovePostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(context => context.User.IsInRole(Roles.Editor));
    }

    public static void CanRejectPostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(context => context.User.IsInRole(Roles.Editor));
    }
}