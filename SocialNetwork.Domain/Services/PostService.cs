using System.Data;
using SocialNetwork.Domain.Commands.Post;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Post;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Services;

public class PostService : IPostRepository
{
    private readonly IDbConnection _dbConnection;

    public PostService(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public ICommandResult<int> Execute(PostCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            int postId = Convert.ToInt32(_dbConnection.ExecuteScalar("CSP_AddPost", true, command));

            _dbConnection.Close();
            return ICommandResult<int>.Success(postId);
        }
        catch (Exception e)
        {
            return ICommandResult<int>.Failure(e.Message);
        }
    }
    
    public IEnumerable<IGrouping<IPost, PostModel>> Execute(PostListQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<PostModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostsGroupByComment", record => record.ToPost(), true, query).ToList();
        
        _dbConnection.Close();
        return models.GroupBy(c => c.Posts);
    }

    public IEnumerable<IGrouping<IPost, PostModel>>  Execute(PostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<PostModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostGroupByComment", record => record.ToPost(), true, query).ToList();
        
        _dbConnection.Close();
        return models.GroupBy(c => c.Posts);
    }
}
