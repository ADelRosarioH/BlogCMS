namespace BlogCMS.Infrastructure.Interfaces;

public interface IJwtProvider
{
    Task<string> GenerateUserToken(string userName);
}