using System.Data;
using SocialNetwork.Domain.Commands.Like;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;

public class LikeService: ILikeRepository
{
    private readonly IDbConnection _dbConnection;

    public LikeService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public ICommandResult Execute(LikeCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddLike", true, command);
        
            _dbConnection.Close();
            return ICommandResult.Success();
        }
        catch (Exception e)
        {
            return ICommandResult.Failure(e.Message);
        }
    }

    public ICommandResult Execute(DisLikeCommand command)
    {
        throw new NotImplementedException();
    }
}
