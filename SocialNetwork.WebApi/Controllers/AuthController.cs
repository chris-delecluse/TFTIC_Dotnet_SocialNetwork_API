using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.AppStates;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Dtos.Auth;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IUserConnectionState _connectionState;
    private readonly IAuthHubService _authHubService;

    public AuthController(
        ITokenService tokenService,
        IUserConnectionState connectionState,
        IAuthHubService authHubService,
        IMediator mediator
    )
    {
        _tokenService = tokenService;
        _connectionState = connectionState;
        _authHubService = authHubService;
        _mediator = mediator;
    }

    [HttpPost, Route("local/register"), AllowAnonymous]
    public async Task<IActionResult> Register(RegisterForm form)
    {
        ICommandResult result =
           await  _mediator.Send(new RegisterCommand(form.FirstName, form.LastName, form.Email, form.Password));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "User created successfully."));
    }

    [HttpPost, Route("local/login"), AllowAnonymous]
    public async Task<IActionResult> Login(LoginForm form)
    {
        UserModel? user = await _mediator.Send(new LoginQuery(form.Email, form.Password));

        if (user is null) 
            return Unauthorized(new ApiResponse(401, false, "Invalid Credentials."));

        LoginDto loginDto = user.ToLoginDto(_tokenService.GenerateAccessToken(user));

        _connectionState.AddUserToConnectedList(user.Id);
        await _authHubService.NotifyUserConnectedToFriends(user);
        return Ok(new ApiResponse(200, true, loginDto, "Success"));
    }

    [HttpPost, Route("local/logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        UserInfo user = HttpContext.ExtractDataFromToken();

        _connectionState.RemoveUserToConnectedList(user.Id);
        await _authHubService.NotifyUserDisConnectedToFriends(user);
        return Ok(new ApiResponse(200, true, "User logout successfully."));
    }
}
