using System.Data;
using SocialNetwork.Domain.Commands.Comment;
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
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddComment", true, command);

            _dbConnection.Close();
            return CqsResult.Success();
        }
        catch (Exception e)
        {
            return CqsResult.Failure(e.Message);
        }
    }
}
