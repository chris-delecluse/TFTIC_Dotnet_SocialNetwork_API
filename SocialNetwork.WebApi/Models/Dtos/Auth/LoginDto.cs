namespace SocialNetwork.WebApi.Models.Dtos.Auth;

public class LoginDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Token { get; init; }

    public LoginDto(int id, string firstName, string lastName, string email, string token)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Token = token;
    }
}
