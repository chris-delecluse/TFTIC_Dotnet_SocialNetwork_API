use social_network
go

create procedure [dbo].[CSP_GetCommentById] @id int
as
begin
    select c.id,
           c.userId,
           c.postId,
           c.content,
           c.createdAt,
           up.profilePicture as commentProfilePicture
    from [Comments] c
             left join dbo.UserProfiles up on c.userId = up.userId
    where c.id = @id
end