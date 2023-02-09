using Microsoft.AspNetCore.Identity;

namespace BlogCMS.Infrastructure.Entities;

public class BlogUser : IdentityUser<Guid>
{
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Feedback> Feedbacks { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}