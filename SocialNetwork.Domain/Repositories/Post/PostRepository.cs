using System.Data;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;

namespace SocialNetwork.Domain.Repositories.Post;

public class PostRepository : IPostRepository
{
    private readonly IDbConnection _dbConnection;

    public PostRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<int> Insert(PostCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        int postId = Convert.ToInt32(_dbConnection.ExecuteScalar("CSP_AddPost", true, command));

        _dbConnection.Close();
        return Task.FromResult(postId);
    }

    public Task Update(UpdatePostCommand command)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        _dbConnection.ExecuteNonQuery("CSP_UpdateIsDeletedPost", true, command);

        _dbConnection.Close();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostListQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        IEnumerable<PostModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostsGroupByComment", record => record.ToPost(), true, query)
                .ToList();

        _dbConnection.Close();
        return Task.FromResult(models.GroupBy(c => c.Posts));
    }

    public Task<IEnumerable<IGrouping<IPost, PostModel>>> Find(PostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) 
            _dbConnection.Open();

        IEnumerable<PostModel> models =
            _dbConnection.ExecuteReader("CSP_GetPostGroupByComment", record => record.ToPost(), true, query)
                .ToList();

        _dbConnection.Close();
        return Task.FromResult(models.GroupBy(c => c.Posts));
    }
}
