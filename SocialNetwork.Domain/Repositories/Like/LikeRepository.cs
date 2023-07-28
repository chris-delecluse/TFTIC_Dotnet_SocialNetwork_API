using System.Data;
using SocialNetwork.Domain.Commands.Commands.Like;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Like;

public class LikeRepository : ILikeRepository
{
    private readonly IDbConnection _dbConnection;

    public LikeRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<ICommandResult> Insert(LikeCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddLike", true, command);
        
            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
    }

    public Task<ICommandResult> Remove(RemoveLikeCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_RemoveLike", true, command);
            
            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
    }
}
