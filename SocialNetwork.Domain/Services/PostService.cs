using System.Data;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;

public class PostService : IPostRepository
{
    private readonly IDbConnection _dbConnection;

    public PostService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public CqsResult Execute(PostCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open)
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_AddPost", true, command);

            _dbConnection.Close();
            return CqsResult.Success();
        }
        catch (Exception e)
        {
            return CqsResult.Failure(e.Message);
        }
    }

    public IEnumerable<PostEntity> Execute(AllPostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        IEnumerable<PostEntity> posts = _dbConnection
                .ExecuteReader("CSP_GetAllPostByUserId", record => record.ToPost(), true, query)
                .ToList();

        _dbConnection.Close();
        return posts;
    }

    public PostEntity? Execute(PostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        PostEntity? post = _dbConnection
            .ExecuteReader("CSP_GetPostByUserId", record => record.ToPost(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return post;
    }
}
