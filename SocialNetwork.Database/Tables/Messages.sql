use social_network
go

create table [Messages]
(
    id              int identity (1, 1) not null,
    senderUserId    int                 not null,
    recipientUserId int                 not null,
    content         nvarchar(max)       not null,
    createdAt       datetime2(7)        not null default (sysdatetime()),

    constraint PK_Message primary key (id),
    constraint FK_Message_Sender foreign key (senderUserId) references [Users] (id),
    constraint FK_Message_Recipient foreign key (recipientUserId) references [Users] (id)
)