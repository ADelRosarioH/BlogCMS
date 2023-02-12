using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Interfaces;

public interface IPostService
{
    Task<ICollection<PostViewModel>> GetApprovedPosts();
    Task<PostViewModel> GetPostById(Guid postId);
    Task<ICollection<PostViewModel>> GetCurrentUserPosts();
    Task<ICollection<PostViewModel>> GetPostsByStatus(PostStatus status);
    Task<PostViewModel> CreateNewPost(NewPostViewModel model);
    Task<PostViewModel> UpdatePost(Guid postId, UpdatePostViewModel model);
    Task<PostViewModel> ChangePostStatus(Guid postId, PostStatus newStatus);
    Task<PostViewModel> RejectPost(Guid postId, string comment);
    Task<bool> IsPostAuthor(Guid userId, Guid postId);
    
}