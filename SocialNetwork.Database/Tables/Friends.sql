use social_network
go

create table [Friends]
(
    id          int identity (1, 1) not null,
    requestId   int                 not null,
    responderId int                 not null,
    state       nvarchar(20)        not null,
    createdAt   datetime2(7)        not null default (sysdatetime()),

    constraint PK_Friends primary key (id),
    constraint FK_Friends_Users1 foreign key (requestId) references [Users] (id),
    constraint FK_Friends_Users2 foreign key (responderId) references [Users] (id)
)