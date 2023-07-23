using System.Data;
using SocialNetwork.Domain.Commands.Comment;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Comment;
using SocialNetwork.Domain.Repositories;
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

    public IEnumerable<int> Execute(CommentUserIdListByPostQuery idListQuery)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<int> userIds = _dbConnection.ExecuteReader("CSP_GetUserIdsFromCommentByPostId",
                record => record.ToCommentUserId(),
                true,
                idListQuery
            )
            .ToList();

        _dbConnection.Close();
        return userIds;
    }
}
