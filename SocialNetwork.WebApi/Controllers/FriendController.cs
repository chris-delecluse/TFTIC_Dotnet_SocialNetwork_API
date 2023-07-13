using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Friend;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Token;
using SocialNetwork.WebApi.Models.Forms.Friend;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/friend"), Authorize]
public class FriendController : ControllerBase
{
    private readonly IFriendRepository _friendService;
    private readonly ITokenService _tokenService;

    public FriendController(IFriendRepository friendService, ITokenService tokenService)
    {
        _friendService = friendService;
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult Add(FriendForm form)
    {
        CqsResult result = _friendService.Execute(new FriendCommand(_tokenService.ExtractUserIdFromToken(HttpContext),
                form.UserId,
                EFriendState.Pending
            )
        );

        if (result.IsFailure) 
            return BadRequest(new { result.Message });

        return Created("", new { Message = "Friend request send successfully." });
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_friendService.Execute(new FriendListQuery(_tokenService.ExtractUserIdFromToken(HttpContext)))
            .ToFriendDto()
        );
    }

    [HttpPatch]
    public IActionResult Update(UpdateFriendRequestForm form)
    {
        CqsResult result = _friendService.Execute(new UpdateFriendStateCommand(form.RequestId,
                _tokenService.ExtractUserIdFromToken(HttpContext),
                form.IsAccepted ? EFriendState.Accepted : EFriendState.Rejected
            )
        );

        if (result.IsFailure) 
            return BadRequest(new { result.Message });

        return Accepted(new { Message = "Friend request updated successfully." });
    }
}
