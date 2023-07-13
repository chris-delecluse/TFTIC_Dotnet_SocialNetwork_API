using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Token;
using SocialNetwork.WebApi.Models.Forms.Post;
using SocialNetwork.WebApi.Models.Mappers;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/post"), Authorize]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postService;
    private readonly ITokenService _tokenService;

    public PostController(IPostRepository postService, ITokenService tokenService)
    {
        _postService = postService;
        _tokenService = tokenService;
    }

    [HttpPost]
    public IActionResult Add(PostForm form)
    {
        CqsResult result =
            _postService.Execute(new PostCommand(form.Content, _tokenService.ExtractUserIdFromToken(HttpContext)));

        if (result.IsFailure)
            return BadRequest(new { result.Message });

        return Created("", new { Message = "Post Added successfully." });
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_postService
            .Execute(new AllPostQuery(_tokenService.ExtractUserIdFromToken(HttpContext)))
            .ToPostDto()
        );
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        PostEntity? post = _postService.Execute(new PostQuery(id, _tokenService.ExtractUserIdFromToken(HttpContext)));

        if (post is null) return NotFound(new { Message = $"Post with id '{id}' does not exists." });

        return Ok(post.ToPostDto());
    }
}
