using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Services;

public class PostService : IPostService
{
    public Task GetPosts()
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetPostById(Guid postId)
    {
        throw new NotImplementedException();
    }

    public Task GetUserPosts()
    {
        throw new NotImplementedException();
    }

    public Task GetPostsByStatus(PostStatus status)
    {
        throw new NotImplementedException();
    }

    public Task CreateNewPost(NewPostViewModel model)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePost()
    {
        throw new NotImplementedException();
    }

    public Task ChangePostStatus(PostStatus newStatus)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPostAuthor(Guid userId, Guid postId)
    {
        throw new NotImplementedException();
    }
}