using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Shared;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models;
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
            _postService.Execute(new PostCommand(form.Content, HttpContext.ExtractDataFromToken().Id));

        if (result.IsFailure) 
            return BadRequest(new ApiResponse(400, false, result.Message));

        return Created("", new ApiResponse(201, true, "Post Added successfully."));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new ApiResponse(200,
                true,
                _postService
                    .Execute(new AllPostQuery(HttpContext.ExtractDataFromToken().Id))
                    .ToPostDto(),
                "Success"
            )
        );
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        PostEntity? post = _postService.Execute(new PostQuery(id,
                HttpContext.ExtractDataFromToken().Id
            )
        );

        if (post is null) 
            return NotFound(new ApiResponse(404, false, $"Post with id '{id}' does not exists."));

        return Ok(new ApiResponse(200, true, post.ToPostDto(), "Success"));
    }
}
