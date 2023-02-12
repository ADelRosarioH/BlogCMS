using AutoMapper;
using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogCMS.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly BlogCMSDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public PostService(BlogCMSDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    
    public async Task<ICollection<PostViewModel>> GetApprovedPosts()
    {
        var approvedPosts = _context.Posts.Where(p => p.Status == PostStatus.Approved);
        return await _mapper
            .ProjectTo<PostViewModel>(approvedPosts)
            .ToListAsync();
    }

    public async Task<PostViewModel> GetPostById(Guid postId)
    {
        var query = _context.Posts.Where(p => p.Id == postId);
        return await _mapper
            .ProjectTo<PostViewModel>(query)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<OwnPostViewModel>> GetCurrentUserPosts()
    {
        var userId = _currentUserService.CurrentUserId;
        var query = _context.Posts.Where(p => p.CreatedByUserId == userId)
            .OrderByDescending(p => p.CreatedAt);
        return await _mapper.ProjectTo<OwnPostViewModel>(query)
            .ToListAsync();
    }

    public async Task<ICollection<PostViewModel>> GetPostsByStatus(PostStatus status)
    {
        var query = _context.Posts.Where(p => p.Status == status);
        return await _mapper.ProjectTo<PostViewModel>(query)
            .ToListAsync();
    }

    public async Task<PostViewModel> CreateNewPost(NewPostViewModel model)
    {
        var entity = _mapper.Map<NewPostViewModel, Post>(model);
        entity.CreatedByUserId = _currentUserService.CurrentUserId;
        await _context.Posts.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<Post, PostViewModel>(entity);
    }

    public async Task<PostViewModel> UpdatePost(Guid postId, UpdatePostViewModel model)
    {
        var entity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        
        if (entity is null)
        {
            throw new Exception("Post not found.");
        }

        entity = _mapper.Map(model, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        
        _context.Posts.Update(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<Post, PostViewModel>(entity);
    }

    public async Task<PostViewModel> ChangePostStatus(Guid postId, PostStatus newStatus)
    {
        var entity = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        
        if (entity is null)
        {
            throw new Exception("Post not found.");
        }

        var prevStatus = entity.Status;
        entity.Status = newStatus;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.StatusLogs.AddAsync(new PostStatusLog
        {
            PostId = postId,
            CreatedByUserId = _currentUserService.CurrentUserId,
            PrevPostStatus = prevStatus,
            NextPostStatus = newStatus,
        });
        
        _context.Posts.Update(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<Post, PostViewModel>(entity);
    }

    public async Task<PostViewModel> RejectPost(Guid postId, string comment)
    {
        var post = await ChangePostStatus(postId, PostStatus.Rejected);
        
        await _context.Feedbacks.AddAsync(new Feedback
        {
            PostId = postId,
            Comment = comment,
            CreatedByUserId = _currentUserService.CurrentUserId
        });
        
        await _context.SaveChangesAsync();
        
        return post;
    }

    public async Task<bool> IsPostAuthor(Guid userId, Guid postId)
    {
        return await _context.Posts.AnyAsync(p => p.CreatedByUserId == userId &&
                                                  p.Id == postId);
    }
}