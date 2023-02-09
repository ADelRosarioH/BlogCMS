namespace BlogCMS.Infrastructure.Models;

public class OwnPostViewModel : PostViewModel
{
    public ICollection<PostFeedbackViewModel> Feedbacks { get; set; }
}