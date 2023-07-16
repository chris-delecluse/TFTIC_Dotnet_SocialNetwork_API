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

    public CommentService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public CqsResult Execute(CommentCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddComment", true, command);

            _dbConnection.Close();
            return CqsResult.Success();
        }
        catch (Exception e)
        {
            return CqsResult.Failure(e.Message);
        }
    }

    public IEnumerable<int> Execute(CommentUserIdListByPostIdQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        IEnumerable<int> userIds = _dbConnection.ExecuteReader("CSP_GetUserIdsFromCommentByPostId",
                record => record.ToCommentUserId(),
                true,
                query
            )
            .ToList();

        _dbConnection.Close();
        return userIds;
    }
}
