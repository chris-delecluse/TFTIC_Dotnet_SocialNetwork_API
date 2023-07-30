using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands;
using SocialNetwork.Domain.Commands.Commands.Comment;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Forms.Comment;
using SocialNetwork.WebApi.Models.Forms.Post;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Tools;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostHubService _postHubService;
    private readonly ICommentHubService _commentHubService;
    private readonly IMediator _mediator;

    public PostController(
        IPostHubService postHubService,
        ICommentHubService commentHubService,
        IMediator mediator
    )
    {
        _postHubService = postHubService;
        _commentHubService = commentHubService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts([FromQuery] int? offset, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> query =
            await _mediator.Send(new PostListQuery(offset ?? 0, 10, isDeleted));

        return Ok(new ApiResponse(200, true, query.ToPostDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> query = await _mediator.Send(new PostQuery(id, isDeleted));

        if (!query.Any()) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, query.First().ToPostDto(), "Success"));
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(PostForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> command = await _mediator.Send(new PostCommand(form.Content, user.Id));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        IEnumerable<IGrouping<IPost, PostModel>> hubResponse = await _mediator.Send(new PostQuery(command.Data, false));

        await _postHubService.NotifyNewPostToFriends(user, new HubResponse("new_post", hubResponse.First().ToPostDto()));
        return Created("", new ApiResponse(201, true, command.Message));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePost(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new UpdatePostCommand(id, user.Id, true));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return NoContent();
    }

    [HttpGet("{id}/comment")]
    public async Task<IActionResult> GetPostComments(int id)
    {
        IEnumerable<CommentModel> query = await _mediator.Send(new CommentListByPostQuery(id));

        return Ok(new ApiResponse(200, true, query.ToCommentDto()));
    }

    [HttpPost("{id}/comment")]
    public async Task<IActionResult> AddPostComment(int id, CommentForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> command = await _mediator.Send(new CommentCommand(form.Content, id, user.Id));
        object hubMessage = new { Id = command.Data, PostId = id, form.Content };

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        await _commentHubService.NotifyNewCommentToPost(user, id, new HubResponse("add_comment", hubMessage));
        return Created("", new ApiResponse(201, true, command.Message));
    }

    [HttpPost("{id}/like")]
    public async Task<IActionResult> AddPostLike(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new LikeCommand(id, user.Id));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        var hubResponse = new { user.Id, user.FirstName, user.LastName, PostId = id };

        await _postHubService.NotifyLikeToPost(id, new HubResponse("new_like", hubResponse));
        return Created("", new ApiResponse(201, true, command.Message));
    }

    [HttpDelete("{id}/like")]
    public async Task<IActionResult> RemovePostLike(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult command = await _mediator.Send(new RemoveLikeCommand(id, user.Id));

        if (command.IsFailure) 
            return BadRequest(new ApiResponse(400, false, command.Message));

        return NoContent();
    }
}
