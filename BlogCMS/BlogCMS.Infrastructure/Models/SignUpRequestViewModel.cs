namespace BlogCMS.Infrastructure.Models;

public class SignUpRequestViewModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}