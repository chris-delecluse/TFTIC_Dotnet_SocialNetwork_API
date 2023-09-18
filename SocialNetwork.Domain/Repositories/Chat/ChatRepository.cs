using System.Data;
using SocialNetwork.Domain.Commands.Commands.Chat;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.Chat;

public class ChatRepository : IChatRepository
{
    private readonly IDbConnection _dbConnection;

    public ChatRepository(IDbConnection dbConnection) { _dbConnection = dbConnection; }

    public Task Insert(MessageCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_AddMessage", true, command);

        _dbConnection.Close();
        return Task.CompletedTask;
    }

    public Task<MessageModel?> Find(MessageQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        MessageModel? models = _dbConnection
            .ExecuteReader("CSP_GetUserConversation", record => record.ToMessage(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return Task.FromResult(models);
    }

    public Task<IEnumerable<MessageModel>> Find(MessagesQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<MessageModel> models = _dbConnection
            .ExecuteReader("CSP_GetUserConversation", record => record.ToMessage(), true, query)
            .ToList();

        _dbConnection.Close();
        return Task.FromResult(models);
    }
}
