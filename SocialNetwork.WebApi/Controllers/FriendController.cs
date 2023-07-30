using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models.Dtos;
using SocialNetwork.WebApi.Models.Forms.Friend;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.Models.Models;

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
        TokenUserInfo tokenUser = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new FriendCommand(tokenUser.Id, form.UserId, EFriendState.Pending));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return Created("", new ApiResponse(201, true, command.Message));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        TokenUserInfo tokenUser = HttpContext.ExtractDataFromToken();
        IEnumerable<FriendModel> query = await _mediator.Send(new FriendListQuery(tokenUser.Id));

        return Ok(new ApiResponse(200, true, query.ToFriendDto(), "Success"));
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdateFriendRequestForm form)
    {
        TokenUserInfo tokenUser = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new UpdateFriendRequestCommand(form.RequestId, tokenUser.Id,
                form.IsAccepted ? EFriendState.Accepted : EFriendState.Rejected
            )
        );

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return Accepted(new ApiResponse(202, true, command.Message));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        TokenUserInfo tokenUser = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new RemoveFriendCommand(tokenUser.Id, id));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return NoContent();
    }
}
