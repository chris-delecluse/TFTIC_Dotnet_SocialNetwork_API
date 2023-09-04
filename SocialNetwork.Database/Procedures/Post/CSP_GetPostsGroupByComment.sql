create procedure [dbo].[CSP_GetPostsGroupByComment] @isDeleted bit, @userId int, @offset int = 0, @limit int = 10
as
begin
    with CTE (id, content, createdAt, userId, postId, profilePicture)
             as (select C.id, C.content, C.createdAt, C.userId, C.postId, UP2.profilePicture
                 from (select *,
                              ROW_NUMBER() over (partition by postId order by createdAt desc )
                                  as commentRowNum
                       from Comments) as C
                          join [Posts] as P on P.id = C.postId
                          join dbo.UserProfiles UP2 on C.userId = UP2.userId
                 where C.commentRowNum <= 2),
         likeCount (postId, likeCount)
             as (select l.postId, COUNT(*) from [Likes] as l group by postId),
         commentCount (postId, commentCount)
             as (select c.postId, COUNT(*) from [Comments] as c group by postId)
    select C.id                         as id,
           C.content                    as content,
           C.createdAt                  as createdAt,
           C.userId                     as userId,
           C.profilePicture             as commentProfilePicture,
           P.id                         as postId,
           P.content                    as postContent,
           P.createdAt                  as postCreatedAt,
           P.userId                     as postUserId,
           U.firstname                  as postUserFirstName,
           U.lastName                   as postUserLastName,
           UP.profilePicture            as postUserProfilePicture,
           coalesce(lc.likeCount, 0)    as likeCount,
           coalesce(cc.commentCount, 0) as commentCount,
           iif(exists (select 1 from [Likes] as ll where postId = P.id and ll.userId = @userId), CAST(1 as bit),
               cast(0 as bit))          as userHasLiked
    from (select *
          from Posts
          order by createdAt desc
          offset @offset rows fetch next @limit rows only) P
             left join CTE C on P.id = C.postId
             join [Users] U on U.id = P.userId
             left join [UserProfiles] as UP on U.id = UP.userId
             left join likeCount as lc on p.id = lc.postId
             left join commentCount as cc on p.id = cc.postId
    where P.isDeleted = @isDeleted
end

-- with likeCount (postId, likeCount)
--          as (select l.postId, count(*) from Likes as l group by postId),
--      commentCount (postId, commentCount) as (select c.postId, count(*) from Comments as c group by postId)
-- select p.*, coalesce(lc.likeCount, 0) as likeCount, coalesce(cc.commentCount, 0) as commentCount
-- from Posts as p
--          left join likeCount as lc on p.id = lc.postId
--          left join commentCount as cc on p.id = cc.postId