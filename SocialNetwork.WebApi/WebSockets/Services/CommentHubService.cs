using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.WebApi.Infrastructures.Security;
using SocialNetwork.WebApi.WebSockets.Bases;
using SocialNetwork.WebApi.WebSockets.Hubs;
using SocialNetwork.WebApi.WebSockets.Interfaces;

namespace SocialNetwork.WebApi.WebSockets.Services;

public class CommentHubService : BasicHubTools, ICommentHubService
{
    private readonly ICommentRepository _commentService;
    private readonly IHubContext<CommentHub, IBaseHub> _postContext;

    public CommentHubService(ICommentRepository commentService, IHubContext<CommentHub, IBaseHub> postContext)
    {
        _commentService = commentService;
        _postContext = postContext;
    }

    public void Notify(UserInfo user, int postId)
    {
        foreach (int targetUserId in GetUserIdsFromCommentBasedOnPostId(postId))
        {
            Console.WriteLine(targetUserId);
            
            SendGroupMessage("CommentGroup",
                targetUserId,
                $"{user.FirstName} {user.LastName} has added a new comment to post: {postId}",
                _postContext
            );
        }
    }

    private IEnumerable<int> GetUserIdsFromCommentBasedOnPostId(int postId) =>
        _commentService.Execute(new CommentUserIdListByPostIdQuery(postId));
}
