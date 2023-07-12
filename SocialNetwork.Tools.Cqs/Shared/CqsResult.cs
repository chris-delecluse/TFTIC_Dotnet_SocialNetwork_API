namespace SocialNetwork.Tools.Cqs.Shared;

public class CqsResult
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public string? Message { get; init; }

    private CqsResult(bool isSuccess, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static CqsResult Success() => new CqsResult(true);

    public static CqsResult Failure(string message) => new CqsResult(false, message);
}
