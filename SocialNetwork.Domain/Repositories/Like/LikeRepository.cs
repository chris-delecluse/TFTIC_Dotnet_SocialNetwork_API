using System.Data;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.Like;

public class LikeRepository : ILikeRepository
{
    private readonly IDbConnection _dbConnection;

    public LikeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task Insert(LikeCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_AddLike", true, command);

        _dbConnection.Close();
        return Task.CompletedTask;
    }

    public Task Remove(RemoveLikeCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_RemoveLike", true, command);

        _dbConnection.Close();
        return Task.CompletedTask;
    }
}
