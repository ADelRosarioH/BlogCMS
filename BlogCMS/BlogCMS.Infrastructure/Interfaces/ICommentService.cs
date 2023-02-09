using BlogCMS.Infrastructure.Models;

namespace BlogCMS.Infrastructure.Interfaces;

public interface ICommentService
{
    Task<PostCommentViewModel> CommentOnPost(Guid postId, string comment);
}