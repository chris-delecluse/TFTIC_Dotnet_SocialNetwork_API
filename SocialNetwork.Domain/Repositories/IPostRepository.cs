using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IPostRepository :
    ICommandHandler<PostCommand, int>,
    IQueryHandler<AllPostQuery, IEnumerable<PostEntity>>,
    IQueryHandler<PostQuery, PostEntity?> { }
