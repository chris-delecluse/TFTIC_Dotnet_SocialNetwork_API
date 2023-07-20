using System.Data;
using SocialNetwork.Domain.Commands.Auth;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Auth;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;
public class AuthService : IAuthRepository
{
    private readonly IDbConnection _dbConnection;

    public AuthService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public ICommandResult Execute(RegisterCommand command)
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

    public UserModel? Execute(LoginQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open)
            _dbConnection.Open();

        UserModel? user = _dbConnection
            .ExecuteReader("CSP_Login", record => record.ToPublicUser(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return user;
    }
}
