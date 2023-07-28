using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Friend;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/friend"), Authorize]
public class FriendController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add(FriendForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = await _mediator.Send(new FriendCommand(user.Id, form.UserId, EFriendState.Pending));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Friend request send successfully."));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        IEnumerable<FriendModel> friends = await _mediator.Send(new FriendListQuery(user.Id));

        return Ok(new ApiResponse(200, true, friends.ToFriendDto(), "Success"));
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdateFriendRequestForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = await _mediator.Send(new UpdateFriendRequestCommand(form.RequestId,
                user.Id,
                form.IsAccepted ? EFriendState.Accepted : EFriendState.Rejected
            )
        );

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Accepted(new ApiResponse(202, true, "Friend request updated successfully."));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = await _mediator.Send(new RemoveFriendCommand(user.Id, id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return NoContent();
    }
}
