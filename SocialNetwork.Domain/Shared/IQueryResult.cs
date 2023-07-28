namespace SocialNetwork.Domain.Shared;

public interface IQueryResult<T>
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string Message { get; }
    T Data { get; }
}
