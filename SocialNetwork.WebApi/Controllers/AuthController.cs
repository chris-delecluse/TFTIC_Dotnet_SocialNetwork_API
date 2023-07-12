using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.SignalR;
using SocialNetwork.WebApi.Infrastructures.SignalR.Hubs;
using SocialNetwork.WebApi.Infrastructures.SignalR.StronglyTypedHubs;
using SocialNetwork.WebApi.Infrastructures.Token;
using SocialNetwork.WebApi.Models.Forms.Auth;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/auth"), AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _authService;
    private readonly IFriendRepository _friendService;
    private readonly IHubService _hubService;
   // private readonly IHubContext<AuthHub, IHub> _authHubContext;

    public AuthController(
        ITokenService tokenService,
        IAuthRepository authService,
        IFriendRepository friendService,
        //IHubContext<AuthHub, IHub> authHubContext,
        IHubService hubService
    )
    {
        _tokenService = tokenService;
        _authService = authService;
        _friendService = friendService;
       // _authHubContext = authHubContext;
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

        if (user is null) return Unauthorized(new { Message = "Invalid Credentials." });

        _hubService.NotifyUserConnectionToFriends(user, _friendService);
        
        //NotifyUserConnectionToFriends(user);

        return Ok(user.ToLoginDto(_tokenService.GenerateAccessToken(user)));
    }

    // private void NotifyUserConnectionToFriends(UserEntity user)
    // {
    //     IEnumerable<FriendEntity> friendList = _friendService.Execute(
    //         new FriendListByStateQuery(user.Id, EFriendState.Accepted)
    //     );
    //
    //     foreach (FriendEntity friend in friendList)
    //     {
    //         if (friend.ResponderId != user.Id)
    //         {
    //             string friendsGroup = $"FriendsGroup_{friend.ResponderId}";
    //
    //             _authHubContext.Clients.Group(friendsGroup)
    //                 .AddToGroup(friendsGroup);
    //
    //             _authHubContext.Clients.Group(friendsGroup)
    //                 .ReceiveMessage($"User {user.FirstName} {user.Lastname} has connected.");
    //         }
    //     }
    // }
}
