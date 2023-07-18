using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Like;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Like;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/likes"), Authorize]
public class LikeController : ControllerBase
{
    private readonly ILikeRepository _likeService;

    public LikeController(ILikeRepository likeService)
    {
        _likeService = likeService;
    }

    [HttpPost]
    public IActionResult Add(LikeForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = _likeService.Execute(new LikeCommand(form.PostId, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Like added successfully."));
    }
}
