namespace SocialNetwork.WebApi.Models.Forms.Message;

public class HubMessageForm
{
    public int From { get; init; }
    public int To { get; init; }
    public string Content { get; init; }
}
