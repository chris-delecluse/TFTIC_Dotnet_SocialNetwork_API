using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Token;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.Services.Auth;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/auth"), AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _authService;
    private readonly IFriendRepository _friendService;
    private readonly IAuthHubService _hubService;

    public AuthController(
        ITokenService tokenService,
        IAuthRepository authService,
        IFriendRepository friendService,
        IAuthHubService hubService
    )
    {
        _tokenService = tokenService;
        _authService = authService;
        _friendService = friendService;
        _hubService = hubService;
    }

    [HttpPost, Route("local/register")]
    public IActionResult Register(RegisterForm form)
    {
        CqsResult result = _authService.Execute(
            new RegisterCommand(form.FirstName, form.LastName, form.Email, form.Password)
        );

        if (result.IsFailure) return BadRequest(new { result.Message });

        return Created("", new { Message = "User created successfully." });
    }

    [HttpPost, Route("local/login")]
    public IActionResult Login(LoginForm form)
    {
        UserEntity? user = _authService.Execute(new LoginQuery(form.Email, form.Password));

        if (user is null) 
            return Unauthorized(new { Message = "Invalid Credentials." });

        _hubService.NotifyUserConnectionToFriends(user);
        
        return Ok(user.ToLoginDto(_tokenService.GenerateAccessToken(user)));
    }
}
