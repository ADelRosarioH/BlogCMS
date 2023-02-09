using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCMS.WebAPI.Controllers;

public class PostsController : BaseController
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;

    public PostsController(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ICollection<PostViewModel>>> Get([FromQuery]PostStatus? status)
    {
        var posts =
            status.HasValue
                ? await _postService.GetPostsByStatus(status.Value)
                : await _postService.GetApprovedPosts();

        return Ok(posts);
    }
    
    [HttpGet("")]
    public async Task<ActionResult<ICollection<OwnPostViewModel>>> GetCurrentUserPosts()
    {
        var posts = await _postService.GetCurrentUserPosts();
        return Ok(posts);
    }
    
    [HttpGet("postId:guid")]
    public async Task<ActionResult<ICollection<PostViewModel>>> GetPostById(Guid postId)
    {
        var post = await _postService.GetPostById(postId);

        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }

    [HttpPost]
    [Authorize(policy: Policies.CanCreateNewPostPolicy)]
    public async Task<ActionResult<PostViewModel>> Create([FromBody]NewPostViewModel model)
    {
        var newPost = await _postService.CreateNewPost(model);
        return Ok(newPost);
    }

    [HttpPut("{postId:guid}")]
    [Authorize(policy: Policies.CanUpdatePostPolicy)]
    public async Task<ActionResult<PostViewModel>> Update(Guid postId, [FromBody]UpdatePostViewModel model)
    {
        var post = await _postService.UpdatePost(postId, model);
        return Ok(post);
    }
    
    [HttpPost("{postId:guid}/submit")]
    [Authorize(policy: Policies.CanSubmitPostPolicy)]
    public async Task<ActionResult<PostViewModel>> Submit(Guid postId)
    {
        var post = await _postService.ChangePostStatus(postId, PostStatus.Pending);
        return Ok(post);
    }
    
    [HttpPost("{postId:guid}/approve")]
    [Authorize(policy: Policies.CanApprovePostPolicy)]
    public async Task<ActionResult<PostViewModel>> Approve(Guid postId)
    {
        var post = await _postService.ChangePostStatus(postId, PostStatus.Approved);
        return Ok(post);
    }

    [HttpPost("{postId:guid}/reject")]
    [Authorize(policy: Policies.CanRejectPostPolicy)]
    public async Task<ActionResult<PostViewModel>> Reject(Guid postId, [FromBody]RejectPostViewModel model)
    {
        var post = await _postService.RejectPost(postId, model.Comment);
        return Ok(post);
    }
    
    [HttpPost("{postId:guid}/comment")]
    [Authorize(policy: Policies.CanCommentPostPolicy)]
    public async Task<ActionResult<PostCommentViewModel>> Reject(Guid postId, [FromBody]NewPostCommentViewModel model)
    {
        var comment = await _commentService.CommentOnPost(postId, model.Content);
        return Ok(comment);
    }
}