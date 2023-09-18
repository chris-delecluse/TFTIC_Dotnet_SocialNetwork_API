using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Infrastructures.Extensions;
using SocialNetwork.WebApi.Models.Dtos;
using SocialNetwork.WebApi.Models.Forms.Message;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.Controllers;

[ApiController, Route("api/chat"), Authorize]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator) { _mediator = mediator; }

    [HttpPost]
    public async Task<IActionResult> Get(MessageForm form)
    {
        TokenUserInfo userInfo = HttpContext.ExtractDataFromToken();

        IEnumerable<MessageModel> models = await _mediator.Send(new MessagesQuery(userInfo.Id, form.To, 1, 10));

        return Ok(new ApiResponse(200, true, models.ToMessageDto(), "Success"));
    }
}