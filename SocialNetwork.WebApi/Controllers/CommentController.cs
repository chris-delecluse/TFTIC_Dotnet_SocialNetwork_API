using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Comment;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/comment"), Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentService;

    public CommentController(ICommentRepository commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public IActionResult Add(CommentForm form)
    {
        CqsResult result = _commentService.Execute(new CommentCommand(form.Content,
                form.PostId,
                HttpContext.ExtractDataFromToken().Id
            )
        );

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Comment created successfully."));
    }
}
