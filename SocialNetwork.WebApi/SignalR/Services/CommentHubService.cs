using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.WebApi.Models.Models;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Services;

public class CommentHubService : ICommentHubService
{
    private readonly IMediator _mediator;
    private readonly IHubContext<CommentHub, IClientHub> _postContext;

    public CommentHubService(
        IHubContext<CommentHub, IClientHub> postContext,
        IMediator mediator
    )
    {
        _postContext = postContext;
        _mediator = mediator;
    }

    public async Task NotifyNewCommentToPost<T>(TokenUserInfo tokenUser, int postId, T dataToSend)
    {
        foreach (int targetUserId in await GetUserIdsFromCommentBasedOnPostId(postId))
        {
            string groupName = $"CommentGroup_{targetUserId}";
            await _postContext.AddToGroup(groupName);
            await _postContext.SendMessage(groupName, JsonSerializer.Serialize(dataToSend));
        }
    }

    private async Task<IEnumerable<int>> GetUserIdsFromCommentBasedOnPostId(int postId) =>
        await _mediator.Send(new CommentUserIdListByPostQuery(postId));
}
