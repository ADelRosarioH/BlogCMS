using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Helpers.Constants;
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
        var query = GetBaseGetPostsQuery()
            .Where(p => p.Status == PostStatus.Approved);
        
        return _mapper.Map<ICollection<PostViewModel>>(await query.ToListAsync());
    }

    public async Task<PostViewModel> GetPostById(Guid postId)
    {
        var post = await GetBaseGetPostsQuery()
            .FirstOrDefaultAsync(p => p.Id == postId);
        
        return _mapper.Map<PostViewModel>(post);
    }

    public async Task<ICollection<PostViewModel>> GetCurrentUserPosts()
    {
        var userId = _currentUserService.CurrentUserId;
        var query = GetBaseGetPostsQuery().Where(p => p.CreatedByUserId == userId)
            .OrderByDescending(p => p.CreatedAt);

        return _mapper.Map<ICollection<PostViewModel>>(await query.ToListAsync());
    }

    public async Task<ICollection<PostViewModel>> GetPostsByStatus(PostStatus status)
    {
        var query = GetBaseGetPostsQuery()
            .Where(p => p.Status == status);

        return _mapper.Map<ICollection<PostViewModel>>(await query.ToListAsync());
    }

    private IQueryable<Post> GetBaseGetPostsQuery()
    {
        var currentUserId = _currentUserService.CurrentUserId;
        var userIsEditor = _currentUserService.IsInRole(Roles.Editor).Result;

        var query = _context.Posts
            .Include(p => p.CreatedByUser)
            .Include(p => p.Comments)
            .Include(p => p.Feedbacks.Where(f => f.Post.CreatedByUserId == currentUserId || userIsEditor))
            .AsQueryable();

        return query;
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