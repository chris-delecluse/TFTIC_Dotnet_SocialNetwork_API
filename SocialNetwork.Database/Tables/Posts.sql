use social_network
go

create table [Posts]
(
    id        int identity (1, 1) not null,
    content   nvarchar(max)       not null,
    createdAt datetime2(7)        not null default (sysdatetime()),
    userId    int                 not null,
    isDeleted bit                 not null default (0),

    constraint PK_Posts primary key (id),
    constraint FK_Posts_Users foreign key (userId) references [Users] (id)
)