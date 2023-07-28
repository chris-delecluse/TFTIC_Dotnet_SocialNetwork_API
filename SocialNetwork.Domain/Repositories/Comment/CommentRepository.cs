using System.Data;
using SocialNetwork.Domain.Commands.Commands.Comment;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Comment;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Comment;

public class CommentRepository : ICommentRepository
{
    private readonly IDbConnection _dbConnection;

    public CommentRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<ICommandResult<int>> Insert(CommentCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            int commentId = Convert.ToInt32(_dbConnection.ExecuteScalar("CSP_AddComment", true, command));

            _dbConnection.Close();
            return Task.FromResult(ICommandResult<int>.Success(commentId));
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult<int>.Failure(e.Message));
        }
    }

    public Task<IEnumerable<CommentModel>> Find(CommentListByPostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<CommentModel> models =
            _dbConnection.ExecuteReader("CSP_GetCommentListByPost", record => record.ToComment(), true, query)
                .ToList();

        _dbConnection.Close();
        return Task.FromResult(models);
    }

    public Task<IEnumerable<int>> Find(CommentUserIdListByPostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<int> userIds = _dbConnection.ExecuteReader("CSP_GetUserIdsFromCommentByPostId",
                record => record.ToCommentUserId(),
                true,
                query
            )
            .ToList();

        _dbConnection.Close();
        return Task.FromResult(userIds);
    }
}