using System.Data;
using SocialNetwork.Domain.Commands.Commands.Auth;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Auth;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly IDbConnection _dbConnection;

    public AuthRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<UserModel?> GetPublicUser(LoginQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserModel? user = _dbConnection
            .ExecuteReader("CSP_Login", record => record.ToPublicUser(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return Task.FromResult(user);
    }

    public async Task<ICommandResult> RegisterUser(RegisterCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();
            
            _dbConnection.ExecuteNonQuery("CSP_Register", true, command);

            _dbConnection.Close();
            return ICommandResult.Success();
        }
        catch (Exception)
        {
            return ICommandResult.Failure($"{nameof(command.Email)} is already used.");
        }
    }
}
