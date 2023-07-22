using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Tools.Cqs.Commands;

namespace SocialNetwork.Domain.Repositories;

public interface IPostRepository :
    ICommandHandler<PostCommand, int> { }
