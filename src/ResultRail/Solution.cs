namespace ResultRail;

public readonly struct Result
    : IResult
{
    public  bool IsSuccess { get; }
    
    public Error? Error { get; }
    
    private Result(bool isSuccess,
        Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() 
        => new (true, null);
    
    public static Result Fail(string message, Exception? exception = null)
        => new (false, new (message, exception));
}

public readonly struct Result<T>
    : IResult<T> where T : new
{
    public bool IsSuccess { get; }
    public Error? Error { get; }
 
    public T Value { get; }

    public Result(bool isSuccess,
        Error? error,
        T value)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
    }

    public static Result<T> Success(T value)
        => new (true, null, value);
    
    public static Result<T> Fail(string message, Exception? exception = null)
        => new (false, new (message, exception), default!);
}

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure
        => !IsSuccess;
    Error? Error { get; }
    string Message
        => Error?.Message ?? string.Empty;
}

public interface IResult<out T>
    : IResult where T : new()
{
    T Value { get; }
}



public record struct Error(string Message, Exception? Exception);