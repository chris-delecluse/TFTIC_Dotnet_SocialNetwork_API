use social_network
go

create procedure [dbo].[CSP_Register] @firstname nvarchar(50),
                                      @lastname nvarchar(50),
                                      @email nvarchar(384),
                                      @password nvarchar(20)
as
begin
    insert into [Users] (firstname, lastname, email, [password])
    values (@firstname, @lastname, @email,
            HASHBYTES('SHA2_512', CONCAT(dbo.CSF_GetPreSalt(), @password, dbo.CSF_GetPostSalt())))
end