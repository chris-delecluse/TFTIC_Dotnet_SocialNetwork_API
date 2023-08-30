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

    public UserController(IMediator mediator) { _mediator = mediator; }

    [HttpGet("profiles")]
    public async Task<IActionResult> GetMinimalProfileList()
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();
        IEnumerable<UserProfileModel> query = await _mediator.Send(new MinimalProfilesQuery(userInfo.Id));

        return Ok(new ApiResponse(200, true, query.ToMinimalProfileDto(), "Success"));
    }

    [HttpGet, Route("profile")]
    public async Task<IActionResult> GetMinimalPrivateProfile()
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();
        UserProfileModel? query = await _mediator.Send(new MinimalProfileQuery(userInfo.Id));

        if (query is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, query.ToMinimalProfileDto(), "Success"));
    }

    [HttpGet, Route("profile/{id}")]
    public async Task<IActionResult> GetMinimalPublicProfile(int id)
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();

        if (userInfo.Id == id)
            return BadRequest(new ApiResponse(400, false, null, "You are not allowed to access your own profile."));

        UserProfileModel? userProfile = await _mediator.Send(new MinimalProfileQuery(id));

        if (userProfile is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, userProfile.ToMinimalProfileDto(), "Success"));
    }

    [HttpGet, Route("profile/details")]
    public async Task<IActionResult> GetFullPrivateProfile()
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();
        UserProfileModel? userProfile = await _mediator.Send(new FullProfileQuery(userInfo.Id));

        if (userProfile is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, userProfile.ToFullProfileDto(), "Success"));
    }

    [HttpGet, Route("profile/{id}/details")]
    public async Task<IActionResult> GetFullPublicProfile(int id)
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();

        if (userInfo.Id == id)
            return BadRequest(new ApiResponse(400, false, null, "You are not allowed to access your own profile."));

        UserProfileModel? userProfile = await _mediator.Send(new FullPublicProfileQuery(id, userInfo.Id));

        if (userProfile is null)
            return NotFound(new ApiResponse(404, false, null, "An error has occurred, user profile not found."));

        return Ok(new ApiResponse(200, true, userProfile.ToFullPublicProfileDto(), "Success"));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePrivateProfile(UserProfileForm form)
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

        if (command.IsFailure) return BadRequest(new ApiResponse(400, false, command.Message));

        return Accepted(new ApiResponse(202, true, command.Message));
    }
}
