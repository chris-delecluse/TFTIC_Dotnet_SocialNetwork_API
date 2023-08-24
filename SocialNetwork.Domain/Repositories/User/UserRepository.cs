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

    public Task<UserProfileModel> Find(MinimalUserProfileInfoQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserProfileModel userProfile = _dbConnection
            .ExecuteReader("CSP_GetMinimalUserProfileInfo", record => record.ToMinimalUserProfile(), true, query)
            .First();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }
    
    public Task<UserProfileModel> Find(UserProfileInfoQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserProfileModel userProfile = _dbConnection
            .ExecuteReader("CSP_GetUserProfileInfo", record => record.ToUserProfile(), true, query)
            .First();

        _dbConnection.Close();
        return Task.FromResult(userProfile);
    }
}
