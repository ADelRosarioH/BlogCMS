namespace BlogCMS.Infrastructure.Entities;

public class Comment
{
    public Guid Id { get; set; }
    public Guid? PostId { get; set; }
    public string Content { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual BlogUser CreatedByUser { get; set; }
    public virtual Post Post { get; set; }
}