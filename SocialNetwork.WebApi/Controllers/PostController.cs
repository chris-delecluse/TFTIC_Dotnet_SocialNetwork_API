using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.SignalR.Hubs;
using SocialNetwork.WebApi.Models.Forms.Post;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postService;
    private readonly IHubContext<PostHub> _hubContext;

    public PostController(IPostRepository postService, IHubContext<PostHub> hubContext)
    {
        _postService = postService;
        _hubContext = hubContext;
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        int userIdFromToken = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);

        CqsResult result = _postService.Execute(new PostCommand(form.Content, userIdFromToken));

        if (result.IsFailure) 
            return BadRequest(new { result.Message });
            
        return Created("", new { Message = "Post Added successfully." });
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        int userIdFromToken = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);

        return Ok(_postService
            .Execute(new AllPostQuery(userIdFromToken))
            .ToPostDto()
        );
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        int userIdFromToken = int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);

        PostEntity? post = _postService.Execute(new PostQuery(id, userIdFromToken));

        if (post is null) 
            return NotFound(new { Message = $"Post with id '{id}' does not exists." });

        return Ok(post.ToPostDto());
    }
}
