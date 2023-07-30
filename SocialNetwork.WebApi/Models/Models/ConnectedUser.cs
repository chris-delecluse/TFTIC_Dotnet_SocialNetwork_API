namespace SocialNetwork.WebApi.Models.Models;

public class ConnectedUser
{
    public int Id { get; init; }
    public string ContextId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public ConnectedUser(int id, string contextId, string firstName, string lastName)
    {
        Id = id;
        ContextId = contextId;
        FirstName = firstName;
        LastName = lastName;
    }
}

