using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Dtos.Post;
using SocialNetwork.WebApi.Models.Forms.Post;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Tools;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postService;
    private readonly ICommentRepository _commentService;
    private readonly IPostHubService _hubService;

    public PostController(IPostRepository postService, ICommentRepository commentService, IPostHubService hubService)
    {
        _postService = postService;
        _commentService = commentService;
        _hubService = hubService;
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _postService.Execute(new PostCommand(form.Content, user.Id));
        PostDto postDto = new(result.Result, form.Content, user.Id, DateTime.Now);

        if (result.IsFailure) return BadRequest(new ApiResponse(400, false, result.Message));

        _hubService.NotifyNewPostToFriends(user, new HubResponse("new_post", postDto));
        return Created("", new ApiResponse(201, true, "Post Added successfully."));
    }

    [HttpGet]
    public IActionResult Get()
    {
        IEnumerable<IGrouping<IPost, CommentModel>> postsGroupByComment =
            _commentService.Execute(new CommentsGroupByPostIdQuery());

        return Ok(new ApiResponse(200, true, postsGroupByComment.ToPostDto()));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        IEnumerable<IGrouping<IPost, CommentModel>> post = 
            _commentService.Execute(new CommentGroupByPostIdQuery(id)).ToList();

        if (!post.Any()) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, post.First().ToPostDto(), "Success"));
    }
}
