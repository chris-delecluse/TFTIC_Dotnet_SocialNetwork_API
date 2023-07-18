namespace SocialNetwork.Tools.Cqs.Shared;

public interface ICommandResult<TResult> where TResult : struct
{
    TResult Result { get; }
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string? Message { get; }

    static ICommandResult<TResult> Success(TResult result) => new CommandResult<TResult>(true, null, result);

    static ICommandResult<TResult> Failure(string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorMessage, nameof(errorMessage));

        if (errorMessage.Trim() is "") 
            throw new ArgumentException("In case of an error, please provide the reason.");

        return new CommandResult<TResult>(false, errorMessage);
    }
}

public interface ICommandResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string? Message { get; }

    static ICommandResult Success() => new CommandResult(true, null);

    static ICommandResult Failure(string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorMessage, nameof(errorMessage));

        if (errorMessage.Trim() is "") 
            throw new ArgumentException("In case of an error, please provide the reason.");

        return new CommandResult(false, errorMessage);
    }
}
