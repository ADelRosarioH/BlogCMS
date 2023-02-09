using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
using BlogCMS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCMS.WebAPI.Controllers;

public class PostsController : BaseController
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("all")]
    public IActionResult Get([FromQuery]PostStatus? status)
    {
        return Ok();
    }
    
    [HttpGet("")]
    public IActionResult GetUserPosts()
    {
        return Ok();
    }

    [HttpPost]
    [Authorize(policy: Policies.CanCreateNewPostPolicy)]
    public IActionResult Create()
    {
        return Ok();
    }

    [HttpPut("{postId:guid}")]
    [Authorize(policy: Policies.CanUpdatePostPolicy)]
    public IActionResult Update(Guid postId)
    {
        return Ok();
    }
    
    [HttpPost("{postId:guid}/submit")]
    [Authorize(Roles = Roles.Writer)]
    public IActionResult Submit(Guid postId)
    {
        return Ok();
    }
    
    [HttpPost("{postId:guid}/approve")]
    [Authorize(policy: Policies.CanApprovePostPolicy)]
    public IActionResult Approve(Guid postId)
    {
        return Ok();
    }

    [HttpPost("{postId:guid}/reject")]
    [Authorize(policy: Policies.CanRejectPostPolicy)]
    public IActionResult Reject(Guid postId)
    {
        return Ok();
    }
}