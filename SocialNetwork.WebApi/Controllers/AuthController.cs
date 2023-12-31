using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands;
using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.JWT;
using SocialNetwork.WebApi.Infrastructures.Users;
using SocialNetwork.WebApi.Models.Dtos;
using SocialNetwork.WebApi.Models.Dtos.Auth;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.Models.Models;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenService _tokenService;
    private readonly IConnectedUserManager _connectedUserManager;
    private readonly IAuthHubService _authHubService;

    public AuthController(
        IMediator mediator,
        ITokenService tokenService,
        IConnectedUserManager connectedUserManager,
        IAuthHubService authHubService
    )
    {
        _mediator = mediator;
        _tokenService = tokenService;
        _connectedUserManager = connectedUserManager;
        _authHubService = authHubService;
    }

    [HttpPost, Route("local/register"), AllowAnonymous]
    public async Task<IActionResult> Register(RegisterForm form)
    {
        ICommandResult command =
           await  _mediator.Send(new RegisterCommand(form.FirstName, form.LastName, form.Email, form.Password));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return Created("", new ApiResponse(201, true, command.Message));
    }

    [HttpPost, Route("local/login"), AllowAnonymous]
    public async Task<IActionResult> Login(LoginForm form)
    {
        UserModel? query = await _mediator.Send(new LoginQuery(form.Email, form.Password));

        if (query is null) 
            return Unauthorized(new ApiResponse(401, false, "Invalid credentials."));

        LoginDto loginDto = query.ToLoginDto(_tokenService.GenerateAccessToken(query));
        
        _connectedUserManager.AddUserToConnectedList(query.Id, form.WebSocketId ?? "null", query.FirstName, query.LastName);
        await _authHubService.NotifyClientUserConnected(query);
        return Ok(new ApiResponse(200, true, loginDto, "Success"));
    }

    [HttpPost, Route("local/logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        TokenUserInfo tokenUser = HttpContext.ExtractDataFromToken();

        _connectedUserManager.RemoveUserToConnectedList(tokenUser.Id);
        await _authHubService.NotifyClientUserDisconnected(tokenUser);
        return Ok(new ApiResponse(200, true, "User logout successfully."));
    }
}
