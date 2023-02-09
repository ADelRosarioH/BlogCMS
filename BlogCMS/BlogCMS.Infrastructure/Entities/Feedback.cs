namespace BlogCMS.Infrastructure.Entities;

public class Feedback
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Comment { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; }
    public virtual BlogUser CreatedByUser { get; set; }
}