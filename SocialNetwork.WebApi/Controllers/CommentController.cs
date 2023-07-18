using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Comment;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/comment"), Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentService;
    private readonly ICommentHubService _commentHubService;

    public CommentController(ICommentRepository commentService, ICommentHubService commentHubService)
    {
        _commentService = commentService;
        _commentHubService = commentHubService;
    }

    [HttpPost]
    public IActionResult Add(CommentForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _commentService.Execute(new CommentCommand(form.Content, form.PostId, user.Id));
        object hubMessage = new { Id = result.Result, form.PostId, form.Content };
        
        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        _commentHubService.NotifyNewCommentToPost(user, form.PostId, hubMessage);
        return Created("", new ApiResponse(201, true, "Comment created successfully."));
    }
}
