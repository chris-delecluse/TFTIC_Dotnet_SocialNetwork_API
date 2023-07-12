using System.Data;
using SocialNetwork.Domain.Commands.Friend;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Friend;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;

public class FriendService : IFriendRepository
{
    private readonly IDbConnection _dbConnection;

    public FriendService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public CqsResult Execute(FriendCommand command)
    {
        if (command.ResponderId <= 0)
            return CqsResult.Failure("The user request to befriend (for 'responderId') cannot be less than or equal to 0.");

        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

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
            return CqsResult.Success();
        }
        catch (Exception e)
        {
            return CqsResult.Failure(e.Message);
        }
    }

    public CqsResult Execute(UpdateFriendStateCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) 
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_UpdateFriendRequestStatus",
                true,
                new
                {
                    command.RequestId,
                    command.ResponderId,
                    State = Enum.GetName(typeof(EFriendState), command.State)
                }
            );

            if (command.State is EFriendState.Accepted)
                AddFriendsToEachOther(command.ResponderId, command.RequestId);

            _dbConnection.Close();
            return CqsResult.Success();
        }
        catch (Exception e)
        {
            return CqsResult.Failure(e.Message);
        }
    }

    public IEnumerable<FriendEntity> Execute(FriendListQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        IEnumerable<FriendEntity> friendList = _dbConnection
            .ExecuteReader("CSP_GetFriendList", record => record.ToFriend(), true, query)
            .ToList();

        _dbConnection.Close();
        return friendList;
    }

    public IEnumerable<FriendEntity> Execute(FriendListByStateQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        IEnumerable<FriendEntity> friendList = _dbConnection
            .ExecuteReader("CSP_GetFriendListByState",
                record => record.ToFriend(),
                true,
                new 
                {
                    requestId = query.RequestId,
                    State = Enum.GetName(typeof(EFriendState), query.State) 
                }
            )
            .ToList();

        _dbConnection.Close();
        return friendList;
    }

    private void AddFriendsToEachOther(int requestId, int responderId) =>
        Execute(new FriendCommand(requestId, responderId, EFriendState.Accepted));
}
