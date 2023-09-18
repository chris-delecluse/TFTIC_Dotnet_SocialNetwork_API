using System.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Domain.Mappers;

internal static class MessageMapper
{
    internal static MessageModel ToMessage(this IDataRecord record) =>
        new((int)record["id"],
            new((int)record["senderId"],
                (string)record["senderLastName"],
                (string)record["senderFirstName"],
                Mapper.GetValueOrDefault<string>(record, "senderProfilePicture")
            ),
            new((int)record["recipientId"],
                (string)record["recipientLastName"],
                (string)record["recipientFirstName"],
                Mapper.GetValueOrDefault<string>(record, "recipientProfilePicture")
            ),
            (string)record["content"],
            (DateTime)record["createdAt"]
        );
}
