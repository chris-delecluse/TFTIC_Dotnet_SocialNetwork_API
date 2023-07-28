using System.Data;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Friend;

public class FriendRepository : IFriendRepository
{
    private readonly IDbConnection _dbConnection;

    public FriendRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<ICommandResult> Insert(FriendCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddFriend",
                true,
                new
                {
                    command.RequestId,
                    command.ResponderId,
                    State = Enum.GetName(typeof(EFriendState), command.State)
                }
            );

            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        { 
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
    }

    public Task<ICommandResult> Update(UpdateFriendRequestCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_UpdateFriendRequestStatus",
                true,
                new
                {
                    command.RequestId,
                    command.ResponderId,
                    State = Enum.GetName(typeof(EFriendState), command.State)
                }
            );

            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
    }

    public Task<IEnumerable<FriendModel>> Find(FriendListQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<FriendModel> friendList = _dbConnection
            .ExecuteReader("CSP_GetFriendList", record => record.ToFriend(), true, query).ToList();

        _dbConnection.Close();
        return Task.FromResult(friendList);
    }

    public Task<IEnumerable<FriendModel>> Find(FriendListByStateQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<FriendModel> friendList = _dbConnection
            .ExecuteReader("CSP_GetFriendListByState",
                record => record.ToFriend(),
                true,
                new { requestId = query.RequestId, State = Enum.GetName(typeof(EFriendState), query.State) }
            )
            .ToList();

        _dbConnection.Close();
        return Task.FromResult(friendList);
    }

    public Task<ICommandResult> Remove(RemoveFriendCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_RemoveFriend", true, command);
            
            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
    }
}
