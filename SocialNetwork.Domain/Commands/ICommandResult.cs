namespace SocialNetwork.Domain.Commands;

public interface ICommandResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string Message { get; }
}

public interface ICommandResult<T> : ICommandResult
{
    T Data { get; }
}
