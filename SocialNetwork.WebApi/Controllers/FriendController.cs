using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Friend;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Friend;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/friend"), Authorize]
public class FriendController : ControllerBase
{
    private readonly IFriendRepository _friendService;

    public FriendController(IFriendRepository friendService)
    {
        _friendService = friendService;
    }

    [HttpPost]
    public IActionResult Add(FriendForm form)
    {
        CqsResult result = _friendService.Execute(new FriendCommand(HttpContext.ExtractDataFromToken().Id,
                form.UserId,
                EFriendState.Pending
            )
        );

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Friend request send successfully."));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new ApiResponse(200,
                true,
                _friendService.Execute(new FriendListQuery(HttpContext.ExtractDataFromToken().Id))
                    .ToFriendDto(),
                "Success"
            )
        );
    }

    [HttpPatch]
    public IActionResult Update(UpdateFriendRequestForm form)
    {
        CqsResult result = _friendService.Execute(new UpdateFriendStateCommand(form.RequestId,
                HttpContext.ExtractDataFromToken().Id,
                form.IsAccepted ? EFriendState.Accepted : EFriendState.Rejected
            )
        );

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Accepted(new ApiResponse(202, true, "Friend request updated successfully."));
    }
}
