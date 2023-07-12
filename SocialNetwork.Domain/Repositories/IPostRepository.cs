using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Tools.Cqs.Commands;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Repositories;

public interface IPostRepository :
    ICommandHandler<PostCommand>,
    IQueryHandler<AllPostQuery, IEnumerable<PostEntity>>,
    IQueryHandler<PostQuery, PostEntity?> { }
