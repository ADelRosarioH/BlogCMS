namespace BlogCMS.Infrastructure.Settings;

public class JwtSettings
{
    public string SecretKey { get; set; }
    public int ExpiresInMinutes { get; set; }
}