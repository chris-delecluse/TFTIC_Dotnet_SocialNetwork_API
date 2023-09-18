using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.TypedHubs;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public class PostHub : Hub<IPostHub> { }
