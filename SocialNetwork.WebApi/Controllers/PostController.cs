using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Extensions;
using SocialNetwork.WebApi.Models.Forms.Post;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postService;

    public PostController(IPostRepository postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        CqsResult result =
            _postService.Execute(new PostCommand(form.Content, HttpContext.ExtractDataFromToken<int>("Id")));

        if (result.IsFailure)
            return BadRequest(new { result.Message });

        return Created("", new { Message = "Post Added successfully." });
    }

    [HttpGet]
    public IActionResult Get()
    { 
        return Ok(_postService
            .Execute(new AllPostQuery(HttpContext.ExtractDataFromToken<int>("Id")))
            .ToPostDto()
        );
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        PostEntity? post = _postService.Execute(new PostQuery(id, HttpContext.ExtractDataFromToken<int>("Id")));

        if (post is null) 
            return NotFound(new { Message = $"Post with id '{id}' does not exists." });

        return Ok(post.ToPostDto());
    }
}
