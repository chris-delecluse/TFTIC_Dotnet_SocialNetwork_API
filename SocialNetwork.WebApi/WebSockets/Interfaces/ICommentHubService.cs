using SocialNetwork.WebApi.Infrastructures.Security;

namespace SocialNetwork.WebApi.WebSockets.Interfaces;

public interface ICommentHubService
{
    void Notify(UserInfo user, int postId);
}
