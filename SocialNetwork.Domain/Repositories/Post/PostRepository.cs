using System.Data;
using SocialNetwork.Domain.Commands.Commands.Post;
using SocialNetwork.Domain.Mappers;
using SocialNetwork.Domain.Queries.Queries.Post;
using SocialNetwork.Models;
using SocialNetwork.Tools.Ado;
using SocialNetwork.Tools.Cqs.Shared;

namespace SocialNetwork.Domain.Repositories.Post;

public class PostRepository : IPostRepository
{
    private readonly IDbConnection _dbConnection;

    public PostRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task<ICommandResult<int>> Insert(PostCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

            int postId = Convert.ToInt32(_dbConnection.ExecuteScalar("CSP_AddPost", true, command));

            _dbConnection.Close();
            return Task.FromResult(ICommandResult<int>.Success(postId));
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult<int>.Failure(e.Message));
        }
    }

    public Task<ICommandResult> Update(UpdatePostCommand command)
    {
        try
        {
            if (_dbConnection.State is not ConnectionState.Open) 
                _dbConnection.Open();

            _dbConnection.ExecuteNonQuery("CSP_UpdateIsDeletedPost", true, command);

            _dbConnection.Close();
            return Task.FromResult(ICommandResult.Success());
        }
        catch (Exception e)
        {
            return Task.FromResult(ICommandResult.Failure(e.Message));
        }
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
