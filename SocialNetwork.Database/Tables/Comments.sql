use social_network
go

create table [Comments]
(
    id        int identity (1,1) not null,
    content   nvarchar(max)      not null,
    createdAt datetime2(7)       not null default (sysdatetime()),
    postId    int                not null,
    userId    int                not null,

    constraint PK_Comments primary key (id),
    constraint FK_Comments_PostId foreign key (postId) references [Posts] (id),
    constraint FK_Comments_Users foreign key (userId) references [Users] (id)
)