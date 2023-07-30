namespace SocialNetwork.Domain.Commands;

public class CommandResult : ICommandResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }

    private CommandResult(string message, bool isSuccess)
    {
        Message = message;
        IsSuccess = isSuccess;
    }

    public static CommandResult Success(string? message = null) => new(message, true);

    public static CommandResult Failure(string message) => new(message, false);
}

public class CommandResult<T> : ICommandResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }
    public T Data { get; }

    private CommandResult(string message, bool isSuccess, T data)
    {
        Message = message;
        IsSuccess = isSuccess;
        Data = data;
    }

    public static CommandResult<T> Success(T data, string? message = null) => new(message, true, data);

    public static CommandResult<T> Failure(string message) => new(message, false, default(T));
}
