using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
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
                HttpContext.ExtractDataFromToken<int>("Id")
            )
        );

        if (result.IsFailure) 
            return BadRequest(new { result.Message });

        return Created("", new { Message = "Comment created successfully." });
    }
}
