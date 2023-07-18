using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Post;
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

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postService;
    private readonly IPostHubService _hubService;

    public PostController(IPostRepository postService, IPostHubService hubService)
    {
        _postService = postService;
        _hubService = hubService;
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        ICommandResult<int> result = _postService.Execute(new PostCommand(form.Content, user.Id));
        object hubMessage = new { Id = result.Result, form.Content };

        if (result.IsFailure) return BadRequest(new ApiResponse(400, false, result.Message));

        _hubService.NotifyNewPostToFriends(user, hubMessage);
        return Created("", new ApiResponse(201, true, "Post Added successfully."));
    }

    [HttpGet]
    public IActionResult Get()
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        IEnumerable<PostEntity> posts = _postService.Execute(new AllPostQuery(user.Id));

        return Ok(new ApiResponse(200, true, posts.ToPostDto(), "Success"));
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        UserInfo user = HttpContext.ExtractDataFromToken();
        PostEntity? post = _postService.Execute(new PostQuery(id, user.Id));

        if (post is null) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, post.ToPostDto(), "Success"));
    }
}
