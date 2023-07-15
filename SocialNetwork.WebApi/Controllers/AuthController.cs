using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Infrastructures.AppStates;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.WebSockets.Services;

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
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "User created successfully."));
    }

    [HttpPost, Route("local/login"), AllowAnonymous]
    public IActionResult Login(LoginForm form)
    {
        UserEntity? user = _authService.Execute(new LoginQuery(form.Email, form.Password));

        if (user is null) 
            return Unauthorized(new ApiResponse(401, false, "Invalid Credentials."));

        _connectionState.AddUserToConnectedList(user.Id);
        _hubService.NotifyUserConnectedToFriends(user);

        return Ok(new ApiResponse(200,
                true,
                user.ToLoginDto(_tokenService.GenerateAccessToken(user)),
                "Success"
            )
        );
    }

    [HttpPost, Route("local/logout"), Authorize]
    public IActionResult Logout()
    {
        UserInfo user = HttpContext.ExtractDataFromToken();

        _connectionState.RemoveUserToConnectedList(user.Id);
        _hubService.NotifyUserDisConnectedToFriends(user);

        return Ok(new ApiResponse(200, true, "User logout successfully."));
    }
}
