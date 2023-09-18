using SocialNetwork.Models;
using SocialNetwork.WebApi.Models.Dtos.Message;

namespace SocialNetwork.WebApi.Models.Mappers;

internal static class MessageMapper
{
    internal static MessageDto ToMessageDto(this MessageModel model) =>
        new(model.Id,
            new(model.From.Id,
                model.From.FirstName,
                model.From.LastName,
                model.From.ProfilePicture
            ),
            new(model.To.Id,
                model.To.FirstName,
                model.To.LastName,
                model.To.ProfilePicture
            ),
            model.Content,
            model.CreatedAt
        );

    internal static IEnumerable<MessageDto> ToMessageDto(this IEnumerable<MessageModel> models)
    {
        List<MessageDto> dto = new List<MessageDto>();

        foreach (MessageModel item in models)
        {
            dto.Add(item.ToMessageDto());
        }

        return dto;
    }
}
