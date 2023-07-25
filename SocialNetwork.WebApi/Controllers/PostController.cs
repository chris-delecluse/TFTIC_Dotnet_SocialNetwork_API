using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Commands.Like;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.Models;
using SocialNetwork.WebApi.Models.Dtos.Comment;
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
    public IActionResult GetPosts([FromQuery] int? offset, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> postsGroupByComment =
            _postService.Execute(new PostListQuery(offset ?? 0, 10, isDeleted));

        return Ok(new ApiResponse(200, true, postsGroupByComment.ToPostDto()));
    }

    [HttpGet("{id}")]
    public IActionResult GetPost(int id, [FromQuery] bool isDeleted = false)
    {
        IEnumerable<IGrouping<IPost, PostModel>> post =
            _postService.Execute(new PostQuery(id, isDeleted)).ToList();

        if (!post.Any()) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, post.First().ToPostDto(), "Success"));
    }

    [HttpPost]
    public IActionResult AddPost(PostForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _postService.Execute(new PostCommand(form.Content, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        IEnumerable<IGrouping<IPost, PostModel>> hubResponse =
            _postService.Execute(new PostQuery(result.Result, false)).ToList();

        _postHubService.NotifyNewPostToFriends(user, new HubResponse("new_post", hubResponse.First().ToPostDto()));
        return Created("", new ApiResponse(201, true, "Post Added successfully."));
    }
    
    [HttpDelete("{id}")]
    public IActionResult RemovePost(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = _postService.Execute(new UpdatePostCommand(id, user.Id, true));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return NoContent();
    }

    [HttpGet("{id}/comment")]
    public IActionResult GetPostComments(int id)
    {
        IEnumerable<CommentDto> comments = 
            _commentService.Execute(new CommentListByPostQuery(id)).ToCommentDto();
        
        return Ok(new ApiResponse(200, true, comments));
    }
    
    [HttpPost("{id}/comment")]
    public IActionResult AddPostComment(int id, CommentForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _commentService.Execute(new CommentCommand(form.Content, id, user.Id));
        object hubMessage = new { Id = result.Result, id, form.Content };
        
        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        _commentHubService.NotifyNewCommentToPost(user, id, new HubResponse("add_comment", hubMessage));
        return Created("", new ApiResponse(201, true, "Comment created successfully."));
    }

    [HttpPost("{id}/like")]
    public IActionResult AddPostLike(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = _likeService.Execute(new LikeCommand(id, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        var hubResponse = new
        {
            user.Id,  
            user.FirstName, 
            user.LastName, 
            PostId = id
        };

        _postHubService.NotifyLikeToPost(id, new HubResponse("new_like", hubResponse));
        return Created("", new ApiResponse(201, true, "Like added successfully."));
    }
    
    [HttpDelete("{id}/like")]
    public IActionResult RemovePostLike(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult result = _likeService.Execute(new DeleteLikeCommand(id, user.Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));
        
        return NoContent();
    }
}
