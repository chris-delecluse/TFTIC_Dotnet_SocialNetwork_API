namespace SocialNetwork.Domain.Shared;

public class QueryResult<T> : IQueryResult<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure { get; }
    public string Message { get; }
    public T Data { get; }
    
    private QueryResult(T data, string message, bool isSuccess)
    {
        Data = data;
        Message = message;
        IsSuccess = isSuccess;
    }

    public static QueryResult<T> Success(T data) => new(data, null, true);

    public static QueryResult<T> Failure(string message) => new(default(T), message, false);
}
