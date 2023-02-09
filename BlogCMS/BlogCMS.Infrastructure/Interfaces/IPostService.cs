using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Interfaces;

public interface IPostService
{
    Task GetPosts();
    Task<Post> GetPostById(Guid postId);
    Task GetUserPosts();
    Task GetPostsByStatus(PostStatus status);
    Task CreateNewPost(NewPostViewModel model);
    Task UpdatePost();
    Task ChangePostStatus(PostStatus newStatus);
    Task<bool> IsPostAuthor(Guid userId, Guid postId);
    
}