using System.Data;
using SocialNetwork.Domain.Commands.Commands.User;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.User;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task Update(UpdateUserProfileInfoCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_UpdateUserProfileInfo", true, command);

        _dbConnection.Close();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<UserProfileModel>> Find(MinimalProfilesQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        IEnumerable<UserProfileModel> userProfile = _dbConnection
            .ExecuteReader("CSP_GetMinimalProfileList", record => record.ToMinimalUserProfile(), true, query).ToList();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }

    public Task<UserProfileModel> Find(MinimalProfileQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserProfileModel userProfile = _dbConnection
            .ExecuteReader("CSP_GetMinimalProfile", record => record.ToMinimalUserProfile(), true, query)
            .First();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }
    
    public Task<UserProfileModel> Find(FullProfileQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserProfileModel userProfile = _dbConnection
            .ExecuteReader("CSP_GetFullProfile", record => record.ToUserProfile(), true, query)
            .First();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }

    public Task<UserProfileModel> Find(FullPublicProfileQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserProfileModel? userProfile = _dbConnection
            .ExecuteReader("CSP_GetPublicProfileWithFriendStatus", record => record.ToUserPublicProfile(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }
}
