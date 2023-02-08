using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogCMS.Infrastructure.Helpers;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public JwtProvider(IOptions<JwtSettings> jwtSettings, UserManager<IdentityUser<Guid>> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateUserToken(string userName)
    {
        // setup claims
        var user = await _userManager.FindByNameAsync(userName);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // setup signing credentials
        var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
        var secretSecurityKey = new SymmetricSecurityKey(secretKeyBytes);
        var credentials = new SigningCredentials(secretSecurityKey, SecurityAlgorithms.HmacSha256);
        
        // setup jwt
        var opts = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiresInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(opts);
    }
}