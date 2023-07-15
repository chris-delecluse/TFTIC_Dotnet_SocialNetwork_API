namespace SocialNetwork.WebApi.Models;

public class ApiResponse
{
    public int StatusCode { get; init; }
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public object? Data { get; init; }

    public ApiResponse(int statusCode, bool isSuccess, object? data = null, string? message = null)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
    }

    public ApiResponse(int statusCode, bool isSuccess, string? message = null)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Data = null;
        Message = message;
    }
}
