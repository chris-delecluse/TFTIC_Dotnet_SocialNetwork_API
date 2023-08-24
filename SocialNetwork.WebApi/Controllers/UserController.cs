using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands;
using SocialNetwork.Domain.Commands.Commands.User;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models.Dtos;
using SocialNetwork.WebApi.Models.Forms.User;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/user"), Authorize]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Route("/profile")]
    public async Task<IActionResult> GetMinimalUserProfile()
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();
        UserProfileModel userProfile = await _mediator.Send(new MinimalUserProfileInfoQuery(userInfo.Id));

        if (userProfile is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, userProfile.ToMinimalUserProfileDto(), "Success"));
    }
    
    [HttpGet, Route("/profile/details")]
    public async Task<IActionResult> GetUserProfile()
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();
        UserProfileModel userProfile = await _mediator.Send(new UserProfileInfoQuery(userInfo.Id));

        if (userProfile is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, userProfile.ToUserProfileDto(), "Success"));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUserProfile(UserProfileForm form)
    {
        TokenUserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new UpdateUserProfileInfoCommand(user.Id,
                form.ProfilePicture,
                form.Gender,
                form.BirthDate,
                form.Country,
                form.RelationShipStatus
            )
        );

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return Accepted(new ApiResponse(202, true, command.Message));
    }
}
