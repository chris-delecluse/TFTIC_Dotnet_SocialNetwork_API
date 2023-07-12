namespace SocialNetwork.Domain.Entities;

public class UserEntity
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string Lastname { get; init; }
    public string Email { get; init; }
    public string? Password { get; internal set; }

    public UserEntity(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        Lastname = lastName;
        Email = email;
        Password = password;
    }

    public UserEntity(int id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName;
        Lastname = lastName;
        Email = email;
    }
}
