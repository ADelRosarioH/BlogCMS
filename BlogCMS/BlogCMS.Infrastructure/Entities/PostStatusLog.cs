namespace BlogCMS.Infrastructure.Entities;

public class PostStatusLog
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public PostStatus PrevPostStatus { get; set; }
    public PostStatus NextPostStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }

    public virtual Post Post { get; set; }
    public virtual BlogUser CreatedByUser { get; set; }

}