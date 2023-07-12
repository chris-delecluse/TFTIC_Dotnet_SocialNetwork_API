use social_network
go

create procedure [dbo].[CSP_Login] @email nvarchar(384),
                                   @password nvarchar(20)
as
begin
    select id, firstname, lastname, @email as email
    from [Users]
    where email = @email
      and password = HASHBYTES('SHA2_512', CONCAT(dbo.CSF_GetPreSalt(), @password, dbo.CSF_GetPostSalt()));

    return 0
end