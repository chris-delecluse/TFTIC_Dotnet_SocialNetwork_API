namespace SocialNetwork.WebApi.SignalR.Tools;

internal class HubResponse
{
    public string Context { get; init; }
    public object Data { get; init; }

    internal HubResponse(string context, object data)
    {
        Context = context;
        Data = data;
    }
}
