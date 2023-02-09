using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogCMS.Infrastructure.Entities;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlogCMS.Infrastructure.Helpers;

public class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<BlogUser> _userManager;

    public JwtProvider(IOptions<JwtSettings> jwtSettings, UserManager<BlogUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateUserToken(string userName)
    {
        // setup claims
        var user = await _userManager.FindByNameAsync(userName);
        var options = new ClaimsIdentityOptions();
        var claims = new List<Claim>
        {
            new Claim(options.UserIdClaimType, user.Id.ToString()),
            new Claim(options.UserNameClaimType, user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(options.RoleClaimType, role)));

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