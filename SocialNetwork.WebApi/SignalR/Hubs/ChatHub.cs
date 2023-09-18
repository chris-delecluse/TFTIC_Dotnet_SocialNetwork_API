using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Domain.Commands;
using SocialNetwork.Domain.Commands.Commands.Chat;
using SocialNetwork.Domain.Queries.Queries.Chat;
using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Forms.Message;
using SocialNetwork.WebApi.Models.Mappers;
using SocialNetwork.WebApi.SignalR.TypedHubs;

namespace SocialNetwork.WebApi.SignalR.Hubs;

public class ChatHub : Hub<IChatHub>
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task ReceiveMessage(string message) => Clients.All.ReceiveMessage(message);

    public async Task SendMessage(string message)
    {
        try
        {
            HubMessageForm? messageForm = JsonSerializer.Deserialize<HubMessageForm>(message,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            if (messageForm is not null)
            {
                ICommandResult command =
                    await _mediator.Send(new MessageCommand(messageForm.From, messageForm.To, messageForm.Content));

                if (command.IsSuccess)
                {
                    MessageModel? query =
                        await _mediator.Send(new MessageQuery(messageForm.From, messageForm.To));

                    if (query is not null)
                    {
                        await ReceiveMessage(JsonSerializer.Serialize(query.ToMessageDto()));
                    }
                }
            }
        }
        catch (JsonException ex) { Console.WriteLine($"SendMessage method error in ChatHub : {ex.Message}"); }
        catch (Exception ex) { Console.WriteLine(ex.Message); }
    }
}
