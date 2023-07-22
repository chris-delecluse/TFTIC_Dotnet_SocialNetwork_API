using System.Data;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;

public class CommentService : ICommentRepository
{
    private readonly IDbConnection _dbConnection;

    public CommentService(IDbConnection dbConnection) { _dbConnection = dbConnection; }

    public ICommandResult<int> Execute(CommentCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            int commentId = Convert.ToInt32(_dbConnection.ExecuteScalar("CSP_AddComment", true, command));

            _dbConnection.Close();
            return ICommandResult<int>.Success(commentId);
        }
        catch (Exception e) { return ICommandResult<int>.Failure(e.Message); }
    }

    public IEnumerable<int> Execute(CommentsUserIdByPostIdQuery idQuery)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<int> userIds = _dbConnection.ExecuteReader("CSP_GetUserIdsFromCommentByPostId",
                record => record.ToCommentUserId(),
                true,
                idQuery
            )
            .ToList();

        _dbConnection.Close();
        return userIds;
    }

    public IEnumerable<IGrouping<IPost, CommentModel>> Execute(CommentsGroupByPostIdQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<CommentModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostsGroupByComment", record => record.ToComment(), true, query).ToList();
        
        _dbConnection.Close();
        return models.GroupBy(c => c.Posts);
    }

    public IEnumerable<IGrouping<IPost, CommentModel>>  Execute(CommentGroupByPostIdQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<CommentModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostGroupByComment", record => record.ToComment(), true, query).ToList();
        
        _dbConnection.Close();
        return models.GroupBy(c => c.Posts);
    }
}
