namespace CertificationHub.Api.Models;

public class ApiResponse
{
    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

    public string CorrelationId { get; init; } = string.Empty;

    public static ApiResponse Ok(string message, string correlationId)
    {
        return new ApiResponse
        {
            Success = true,
            Message = message,
            CorrelationId = correlationId
        };
    }

    public static ApiResponse Fail(string message, IEnumerable<string> errors, string correlationId)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message,
            Errors = errors.ToArray(),
            CorrelationId = correlationId
        };
    }
}

public sealed class ApiResponse<T> : ApiResponse
{
    public T? Data { get; init; }

    public static ApiResponse<T> Ok(T data, string message, string correlationId)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data,
            CorrelationId = correlationId
        };
    }

    public static new ApiResponse<T> Fail(string message, IEnumerable<string> errors, string correlationId)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors.ToArray(),
            CorrelationId = correlationId
        };
    }
}
