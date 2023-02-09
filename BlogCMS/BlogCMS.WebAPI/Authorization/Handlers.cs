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
            var postService = GetPostService(context);
            var userManager = GetUserManager(context);
            var postId = GetPostId(context);
            
            var currentUser = await userManager.FindByNameAsync(context.User.Identity.Name);
            var currentPost = await postService.GetPostById(postId);

            // Writer can only update his own posts when status is draft or rejected
            return context.User.IsInRole(Roles.Writer) &&
                   await postService.IsPostAuthor(currentUser.Id, postId) &&
                   (currentPost.Status == PostStatus.Draft || currentPost.Status == PostStatus.Rejected);
        });
    }

    public static void CanApprovePostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(async context =>
        {
            var postService = GetPostService(context);
            var postId = GetPostId(context);
            
            var currentPost = await postService.GetPostById(postId);

            return context.User.IsInRole(Roles.Editor) && currentPost.Status == PostStatus.Pending;
        });
    }

    public static void CanRejectPostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(async context =>
        {
            var postService = GetPostService(context);
            var postId = GetPostId(context);
            
            var currentPost = await postService.GetPostById(postId);

            return context.User.IsInRole(Roles.Editor) && currentPost.Status == PostStatus.Pending;
        });
    }
    
    public static void CanSubmitPostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(async context =>
        {
            var postService = GetPostService(context);
            var userManager = GetUserManager(context);
            var postId = GetPostId(context);
            
            var currentUser = await userManager.FindByNameAsync(context.User.Identity.Name);
            var currentPost = await postService.GetPostById(postId);

            // writer can only submit his own posts when status is draft or rejected
            return context.User.IsInRole(Roles.Writer) &&
                   await postService.IsPostAuthor(currentUser.Id, postId) &&
                   (currentPost.Status == PostStatus.Draft || currentPost.Status == PostStatus.Rejected);
        });
    }
    
    public static void CanCommentPostPolicyHandler(AuthorizationPolicyBuilder builder)
    {
        builder.RequireAssertion(async context =>
        {
            var postService = GetPostService(context);
            var postId = GetPostId(context);
            
            var currentPost = await postService.GetPostById(postId);

            return currentPost.Status == PostStatus.Approved;
        });
    }

    private static Guid GetPostId(AuthorizationHandlerContext context)
    {
        var postIdString = (context.Resource as DefaultHttpContext)?
            .HttpContext?
            .Request?
            .RouteValues["postId"] as string ?? string.Empty;

        var postId = Guid.Parse(postIdString);

        return postId;
    }

    private static IPostService GetPostService(AuthorizationHandlerContext context)
    {
        var postService = (context.Resource as DefaultHttpContext)?
            .HttpContext?
            .RequestServices?
            .GetService<IPostService>();

        return postService;
    }

    private static UserManager<BlogUser> GetUserManager(AuthorizationHandlerContext context)
    {
        var userManager = (context.Resource as DefaultHttpContext)?
            .HttpContext?
            .RequestServices?
            .GetService<UserManager<BlogUser>>();

        return userManager;
    }
}