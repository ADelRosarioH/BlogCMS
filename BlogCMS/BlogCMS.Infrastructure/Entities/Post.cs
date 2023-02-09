using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Entities;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public PostStatus Status { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; }
    public virtual ICollection<PostStatusLog> StatusLogs { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual BlogUser CreatedByUser { get; set; }
}