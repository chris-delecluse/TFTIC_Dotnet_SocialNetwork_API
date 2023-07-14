using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Extensions;
using SocialNetwork.WebApi.Infrastructures;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.Services;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _authService;
    private readonly IUserConnectionState _connectionState;
    private readonly IHubService _hubService;

    public AuthController(
        ITokenService tokenService,
        IAuthRepository authService,
        IUserConnectionState connectionState,
        IHubService hubService
    )
    {
        _tokenService = tokenService;
        _authService = authService;
        _connectionState = connectionState;
        _hubService = hubService;
    }

    [HttpPost, Route("local/register"), AllowAnonymous]
    public IActionResult Register(RegisterForm form)
    {
        CqsResult result = _authService.Execute(
            new RegisterCommand(form.FirstName, form.LastName, form.Email, form.Password)
        );

        if (result.IsFailure) 
            return BadRequest(new { result.Message });

        return Created("", new { Message = "User created successfully." });
    }

    [HttpPost, Route("local/login"), AllowAnonymous]
    public IActionResult Login(LoginForm form)
    {
        UserEntity? user = _authService.Execute(new LoginQuery(form.Email, form.Password));

        if (user is null) 
            return Unauthorized(new { Message = "Invalid Credentials." });

        _connectionState.AddUserToConnectedList(user.Id);
        _hubService.NotifyUserConnectionToFriends(user);

        return Ok(user.ToLoginDto(_tokenService.GenerateAccessToken(user)));
    }

    [HttpPost, Route("local/logout"), Authorize]
    public IActionResult Logout()
    {
        int userId = HttpContext.ExtractDataFromToken<int>("Id");
        
        _connectionState.RemoveUserToConnectedList(userId);
        _hubService.NotifyUserDisConnectionToFriends(userId,
            HttpContext.ExtractDataFromToken<string>(ClaimTypes.GivenName),
            HttpContext.ExtractDataFromToken<string>(ClaimTypes.Surname)
        );
        
        return Ok(new { Message = "User logout successfully." });
    }
}
