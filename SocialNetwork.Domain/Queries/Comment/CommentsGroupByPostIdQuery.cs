using SocialNetwork.Models;
using SocialNetwork.Tools.Cqs.Queries;

namespace SocialNetwork.Domain.Queries.Comment;

public class CommentsGroupByPostIdQuery : IQuery<IEnumerable<IGrouping<IPost, CommentModel>>> { }
