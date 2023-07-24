using Microsoft.AspNetCore.SignalR;
using SocialNetwork.WebApi.SignalR.Interfaces;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public class ChatHub : Hub<IClientHub> { }