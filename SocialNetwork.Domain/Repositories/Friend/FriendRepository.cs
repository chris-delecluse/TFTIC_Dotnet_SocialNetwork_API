using System.Data;
using SocialNetwork.Domain.Commands.Commands.Friend;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Friend;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.Friend;

public class FriendRepository : IFriendRepository
{
    private readonly IDbConnection _dbConnection;

    public FriendRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task Insert(FriendCommand command)
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
        return Task.CompletedTask;
    }

    public Task Update(UpdateFriendRequestCommand command)
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
        return Task.CompletedTask;
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

    public Task Remove(RemoveFriendCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_RemoveFriend", true, command);
            
        _dbConnection.Close();
        return Task.CompletedTask;
    }
}
