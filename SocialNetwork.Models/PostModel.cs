namespace SocialNetwork.Models;

// public class PostModel
// {
//     public int Id { get; init; }
//     public string Content { get; init; }
//     public DateTime CreatedAt { get; init; }
//     public int UserId { get; init; }
//
//     public IPost Posts { get; init; }
//
//     public PostModel(
//         int id,
//         string content,
//         DateTime createdAt,
//         int userId,
//         int postId,
//         string postContent,
//         DateTime postCreatedAt,
//         int postUserId,
//         string postUserFirstName,
//         string postUserLastName
//     )
//     {
//         Id = id;
//         Content = content;
//         CreatedAt = createdAt;
//         UserId = userId;
//
//         Posts = new Post(postId, postContent, postCreatedAt, postUserId, postUserFirstName, postUserLastName);
//     }
//
//     private struct Post : IPost
//     {
//         public int Id { get; init; }
//         public string Content { get; init; }
//         public DateTime CreatedAt { get; init; }
//         public int UserId { get; init; }
//         public string UserFirstName { get; init; }
//         public string UserLastName { get; init; }
//
//         public Post(int id, string content, DateTime createdAt, int userId, string userFirstName, string userLastName)
//         {
//             Id = id;
//             Content = content;
//             CreatedAt = createdAt;
//             UserId = userId;
//             UserFirstName = userFirstName;
//             UserLastName = userLastName;
//         }
//     }
// }
//
// public interface IPost
// {
//     public int Id { get; }
//     public string Content { get; }
//     public DateTime CreatedAt { get; }
//     public int UserId { get; }
//     public string UserFirstName { get; }
//     public string UserLastName { get; }
// }

// ---------- v2 below

public class PostModel
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }

    public IPost Posts { get; init; }

    public PostModel(
        int id,
        string content,
        DateTime createdAt,
        int userId,
        int postId,
        string postContent,
        DateTime postCreatedAt,
        int postUserId,
        string postUserFirstName,
        string postUserLastName,
        string postUserProfilePicture,
        int postLikeCount,
        int postCommentCount
    )
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        UserId = userId;

        Posts = new Post(postId,
            postContent,
            postCreatedAt,
            postUserId,
            postUserFirstName,
            postUserLastName,
            postUserProfilePicture,
            postLikeCount,
            postCommentCount
        );
    }

    private struct Post : IPost
    {
        public int Id { get; init; }
        public string Content { get; init; }
        public DateTime CreatedAt { get; init; }
        public int UserId { get; init; }
        public string UserFirstName { get; init; }
        public string UserLastName { get; init; }
        public string UserProfilePicture { get; init; }
        public int LikesCount { get; init; }
        public int CommentCount { get; init; }

        public Post(
            int id,
            string content,
            DateTime createdAt,
            int userId,
            string userFirstName,
            string userLastName,
            string userProfilePicture,
            int likesCount,
            int commentCount
        )
        {
            Id = id;
            Content = content;
            CreatedAt = createdAt;
            UserId = userId;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserProfilePicture = userProfilePicture;
            LikesCount = likesCount;
            CommentCount = commentCount;
        }
    }
}

public interface IPost
{
    public int Id { get; }
    public string Content { get; }
    public DateTime CreatedAt { get; }
    public int UserId { get; }
    public string UserFirstName { get; }
    public string UserLastName { get; }
    public string UserProfilePicture { get; }
    public int LikesCount { get; }
    public int CommentCount { get; }
}
