using BlogCMS.Infrastructure.Entities;

namespace BlogCMS.Infrastructure.Models;

public class PostViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostStatus Status { get; set; }
    public string StatusDescription { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<PostCommentViewModel> Comments { get; set; }
}