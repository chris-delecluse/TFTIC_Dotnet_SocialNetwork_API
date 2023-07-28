using SocialNetwork.Domain.Shared;

public class CommandResult : ICommandResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Message { get; }

    private CommandResult(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static CommandResult Success() => new CommandResult(null, true);

    public static CommandResult Failure(string message) => new CommandResult(message, false);
}

public class CommandResult<T> : ICommandResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Message { get; }
    public T Data { get; }

    private CommandResult(string message, bool isSuccess, T data)
    {
        Message = message;
        IsSuccess = isSuccess;
        Data = data;
    }

    public static CommandResult<T> Success(T data) => new CommandResult<T>(null, true, data);

    public static CommandResult<T> Failure(string message) => new CommandResult<T>(message, false, default(T));
}
