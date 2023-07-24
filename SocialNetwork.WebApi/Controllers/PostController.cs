using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Commands.Like;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
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
    private readonly IPostRepository _postService;
    private readonly ICommentRepository _commentService;
    private readonly ILikeRepository _likeService;
    private readonly IPostHubService _postHubService;
    private readonly ICommentHubService _commentHubService;

    public PostController(
        IPostRepository postService,
        ICommentRepository commentService, 
        ILikeRepository likeRepository,
        IPostHubService postHubService,  
        ICommentHubService commentHubService)
    {
        _postService = postService;
        _commentService = commentService;
        _likeService = likeRepository;
        _postHubService = postHubService;
        _commentHubService = commentHubService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int? offset, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> postsGroupByComment =
            _postService.Execute(new PostListQuery(offset ?? 0, 10, isDeleted));

        return Ok(new ApiResponse(200, true, postsGroupByComment.ToPostDto()));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> post =
            _postService.Execute(new PostQuery(id, isDeleted)).ToList();

        if (!post.Any()) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, post.First().ToPostDto(), "Success"));
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _postService.Execute(new PostCommand(form.Content, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        IEnumerable<IGrouping<IPost, PostModel>> post =
            _postService.Execute(new PostQuery(result.Result, false)).ToList();

        _postHubService.NotifyNewPostToFriends(user, new HubResponse("new_post", post.First().ToPostDto()));
        return Created("", new ApiResponse(201, true, "Post Added successfully."));
    }
    
    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        return NoContent();
    }

    [HttpGet("{id}/comment")]
    public IActionResult Get()
    {
        return Ok();
    }
    
    [HttpPost("{id}/comment")]
    public IActionResult Add(int id, CommentForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _commentService.Execute(new CommentCommand(form.Content, id, user.Id));
        object hubMessage = new { Id = result.Result, id, form.Content };
        
        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        _commentHubService.NotifyNewCommentToPost(user, id, new HubResponse("AddComment", hubMessage));
        return Created("", new ApiResponse(201, true, "Comment created successfully."));
    }

    [HttpPost("{id}/like")]
    public IActionResult Add(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = _likeService.Execute(new LikeCommand(id, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Like added successfully."));
    }
    
    [HttpDelete("{id}/like")]
    public IActionResult Remove(string id)
    {
        return NoContent();
    }

    [HttpPost("comment/{id}/like")]
    public IActionResult Add(Guid lol)
    {
        return Ok();
    }
    
    [HttpDelete("comment/{id}/like")]
    public IActionResult Remove(Guid id)
    {
        return NoContent();
    }
}
