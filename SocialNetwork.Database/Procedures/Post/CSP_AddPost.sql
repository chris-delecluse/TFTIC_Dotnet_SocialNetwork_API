use social_network
go

create procedure [dbo].[CSP_AddPost](
    @content nvarchar(max),
    @userId int
)
as
begin
    insert into [Posts] (content, userId)
    values (@content, @userId) 
    
    select SCOPE_IDENTITY()
end