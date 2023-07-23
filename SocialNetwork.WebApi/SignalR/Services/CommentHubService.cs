using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.SignalR.Extensions;
using SocialNetwork.WebApi.SignalR.Hubs;
using SocialNetwork.WebApi.SignalR.Tools;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Services;

public class CommentHubService : ICommentHubService
{
    private readonly ICommentRepository _commentService;
    private readonly IHubContext<CommentHub, IClientHub> _postContext;

    public CommentHubService(ICommentRepository commentService, IHubContext<CommentHub, IClientHub> postContext)
    {
        _commentService = commentService;
        _postContext = postContext;
    }

    public async Task NotifyNewCommentToPost<T>(UserInfo user, int postId, T dataToSend)
    {
        foreach (int targetUserId in GetUserIdsFromCommentBasedOnPostId(postId))
        {
            string groupName = $"CommentGroup_{targetUserId}";
            await _postContext.AddToGroup(groupName);
            await _postContext.SendMessage(groupName, JsonSerializer.Serialize(dataToSend));
        }
    }

    private IEnumerable<int> GetUserIdsFromCommentBasedOnPostId(int postId) =>
        _commentService.Execute(new CommentUserIdListByPostQuery(postId));
}
