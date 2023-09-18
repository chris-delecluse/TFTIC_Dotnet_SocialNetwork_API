using System.Data;
using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly IDbConnection _dbConnection;

    public AuthRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<UserModel?> Find(LoginQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserModel? user = _dbConnection
            .ExecuteReader("CSP_Login", record => record.ToPublicUser(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return Task.FromResult(user);
    }

    public Task<int> Insert(RegisterCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();
            
        int result =_dbConnection.ExecuteNonQuery("CSP_Register", true, command);

        _dbConnection.Close();
        return Task.FromResult(result);
    }
}
