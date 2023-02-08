using System.Net;
using BlogCMS.Infrastructure.Interfaces;
using BlogCMS.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCMS.WebAPI.Controllers;

public class AuthController : BaseController
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IAuthService _authService;
    
    public AuthController(IJwtProvider jwtProvider, IAuthService authService)
    {
        _jwtProvider = jwtProvider;
        _authService = authService;
    }

    [HttpPost("signin")]
    public async Task<ActionResult> SignIn(SignInRequestViewModel model)
    {
        if (!await _authService.AreCredentialsValid(model.UserName, model.Password))
        {
            return Unauthorized();
        }

        var token = await _jwtProvider.GenerateUserToken(model.UserName);

        return Ok(new SignInResponseViewModel {Token = token});
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequestViewModel model)
    {
        var newUser = await _authService.RegisterNewUser(model.UserName, model.Email, model.Password);
        
        if (newUser is not null)
        {
            return StatusCode((int)HttpStatusCode.Created);
        }

        return BadRequest();
    }
}