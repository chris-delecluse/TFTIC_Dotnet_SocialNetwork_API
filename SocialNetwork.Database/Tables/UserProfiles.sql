use social_network;
go

create table [UserProfiles]
(
    id                 int identity (1, 1),
    userId             int not null unique,
    profilePicture     nvarchar(150),
    gender             nvarchar(40),
    birthDate          datetime2(7),
    country            nvarchar(75),
    relationShipStatus nvarchar(40),

    constraint PK_UserProfileId primary key (id),
    constraint FK_UserProfile_User foreign key (userId) references [Users] (id)
)