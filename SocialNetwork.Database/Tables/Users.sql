use social_network;
go

create table [Users]
(
    id        int identity (1,1) not null,
    firstname nvarchar(50)       not null,
    lastName  nvarchar(50)       not null,
    email     nvarchar(254)      not null,
    password  binary(64)         not null,

    constraint PK_Users primary key (id),
    constraint UQ_Users_Email unique (email)
)