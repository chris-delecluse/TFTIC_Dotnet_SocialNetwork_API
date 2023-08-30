use social_network;
go

create procedure [dbo].[CSP_GetPublicProfileWithFriendStatus](
    @userProfileId int,
    @viewerId int
)
as
begin
    select u.id,
           u.firstname,
           u.lastName,
           u.email,
           up.profilePicture,
           up.backdropImage,
           up.gender,
           up.birthDate,
           up.relationShipStatus,
           up.country,
           (select f.state
            from [Friends] as f
            where f.responderId = @viewerId
              and f.requestId = @userProfileId) as friendRequestStatus,
           f.initiator as isFriendRequestInitiator
    from [Users] as u
             inner join [UserProfiles] as up on u.id = up.userId
             left join [Friends] f on u.id = f.requestId
    where u.id = @userProfileId
end