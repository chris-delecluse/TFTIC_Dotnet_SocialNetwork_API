use social_network
go

create procedure [dbo].[CSP_GetPostsWithDetailsByUserId](
    @userId int
)
as
begin
    select P.id as postId,
           P.content as postContent,
           P.createdAt as postCreatedAt,
           P.userId as postUserId,
           C.id as commentId,
           C.userId as commentUserId,
           C.content as commentContent,
           C.createdAt as commentCreatedAt,
           C.postId as commentPostId
    from [Posts] P
             left join [Comments] C on P.Id = C.PostId
    where P.UserId = @userId
    order by P.Id, C.Id
    
    
    
end