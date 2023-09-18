use social_network
go

create procedure [dbo].[CSP_GetUserConversation] @from int, @to int, @offset int = 1, @limit int = 10
as
begin
    with CTE as (select m.id,
                        m.content,
                        m.createdAt,
                        sender.id                                     as senderId,
                        sender.lastName                               as senderLastName,
                        sender.firstName                              as senderFirstName,
                        senderUP.profilePicture                       as senderProfilePicture,
                        recipent.id                                   as recipientId,
                        recipent.lastName                             as recipientLastName,
                        recipent.firstName                            as recipientFirstName,
                        recipentUP.profilePicture                     as recipientProfilePicture,
                        row_number() over (order by m.createdAt desc) as row_num,
                        count(*) over ()                              as total_count
                 from [Messages] as m
                          join [Users] as sender on m.senderUserId = sender.id
                          join dbo.UserProfiles as senderUP on sender.id = senderUP.userId
                          join [Users] as recipent on m.recipientUserId = recipent.id
                          join dbo.UserProfiles as recipentUP on recipent.id = recipentUP.userId
                 where (m.senderUserId = @from and m.recipientUserId = @to)
                    or (m.senderUserId = @to and m.recipientUserId = @from))
    select id,
           content,
           createdAt,
           senderId,
           senderLastName,
           senderFirstName,
           senderProfilePicture,
           recipientId,
           recipientLastName,
           recipientFirstName,
           recipientProfilePicture,
           total_count
    from CTE
    where row_num between ((@offset - 1) * @limit + 1) and (@offset * @limit)
    order by createdAt desc;
end

exec CSP_GetUserConversation 1003, 1