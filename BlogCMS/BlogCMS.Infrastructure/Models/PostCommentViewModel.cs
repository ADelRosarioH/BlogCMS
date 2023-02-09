namespace BlogCMS.Infrastructure.Models;

public class PostCommentViewModel
{
    public string Content { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}