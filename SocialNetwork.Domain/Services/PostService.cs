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
        catch (Exception e) { return ICommandResult<int>.Failure(e.Message); }
    }

    public IEnumerable<PostModel> Execute(AllPostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<PostModel> posts = _dbConnection
            .ExecuteReader("CSP_GetAllPostByUserId", record => record.ToPost(), true, query)
            .ToList();

        _dbConnection.Close();
        return posts;
    }

    public PostModel? Execute(PostQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        PostModel? post = _dbConnection
            .ExecuteReader("CSP_GetPostByUserId", record => record.ToPost(), true, query)
            .FirstOrDefault();

        _dbConnection.Close();
        return post;
    }

    public IEnumerable<IGrouping<IComment, PostDetailModel>> Execute(AllPostDetailQuery query)
    {
        if (_dbConnection.State is not ConnectionState.Open) _dbConnection.Open();

        IEnumerable<PostDetailModel> posts =
            _dbConnection.ExecuteReader("CSP_GetPostsWithDetailsByUserId", record => record.ToPostDetail(), true, query)
                .ToList();

        _dbConnection.Close();
        return posts.GroupBy(g => g.Comments);
    }
}
