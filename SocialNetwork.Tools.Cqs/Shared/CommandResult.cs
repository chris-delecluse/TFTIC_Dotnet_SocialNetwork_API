namespace SocialNetwork.Tools.Cqs.Shared;

internal class CommandResult<TResult> : CommandResult, ICommandResult<TResult> where TResult : struct
{
    private readonly TResult _result;

    internal CommandResult(bool isSuccess, string? message = null, TResult result = default) : base(isSuccess, message)
    {
        _result = result;
    }

    public TResult Result
    {
        get
        {
            if (IsFailure) 
                throw new InvalidOperationException("No results obtained due to insertion failure.");

            return _result;
        }
    }
}

internal class CommandResult : ICommandResult
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Message { get; }

    public CommandResult(bool isSuccess, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}
