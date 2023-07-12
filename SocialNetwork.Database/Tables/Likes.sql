use social_network
go

create table [Likes]
(
    id     int identity (1,1) not null,
    postId int                not null,
    userId int                not null,

    constraint PK_Likes primary key (id),
    constraint FK_Likes_Posts foreign key (postId) references [Posts] (id),
    constraint FK_Likes_Users foreign key (userId) references [Users] (id)
)