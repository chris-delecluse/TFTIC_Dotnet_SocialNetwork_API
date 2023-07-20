namespace SocialNetwork.Models;

public class LikeModel
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public int UserId { get; init; }
    
    public LikeModel(int id, int postId, int userId)
    {
        Id = id;
        PostId = postId;
        UserId = userId;
    }
}
