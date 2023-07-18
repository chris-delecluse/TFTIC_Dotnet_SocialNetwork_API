using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface ICommentHubService
{
    Task NotifyNewCommentToPost<T>(UserInfo user, int postId, T dataToSend);
}
