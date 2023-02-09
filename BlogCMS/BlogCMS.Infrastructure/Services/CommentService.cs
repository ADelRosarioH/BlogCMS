using AutoMapper;
using BlogCMS.Infrastructure.Context;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly BlogCMSDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CommentService(BlogCMSDbContext context, ICurrentUserService currentUserService, IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }
    
    public async Task<PostCommentViewModel> CommentOnPost(Guid postId, string comment)
    {
        var entity = new Comment
        {
            PostId = postId,
            Content = comment,
            CreatedByUserId = _currentUserService.CurrentUserId,
        };
        
        await _context.Comments.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<Comment, PostCommentViewModel>(entity);
    }
}