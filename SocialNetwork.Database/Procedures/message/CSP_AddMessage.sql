use social_network
go

create procedure [dbo].[CSP_AddMessage](
    @from int,
    @to int,
    @content nvarchar(max)
)
as
begin
    insert into [Messages] (senderUserId, recipientUserId, content)
    values (@from, @to, @content);
end