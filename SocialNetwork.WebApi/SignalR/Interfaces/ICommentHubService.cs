using SocialNetwork.WebApi.Models.Models;

namespace SocialNetwork.WebApi.SignalR.Interfaces;

public interface ICommentHubService
{
    Task NotifyNewCommentToPost<T>(TokenUserInfo tokenUser, int postId, T dataToSend);
}
