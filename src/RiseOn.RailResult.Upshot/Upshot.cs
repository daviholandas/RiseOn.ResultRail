namespace RiseOn.RailResult.Upshot;

public readonly struct Upshot
    : IUpshot
{
    public  bool IsSuccess { get; }
    
    public bool IsFailure
        => !IsSuccess;

    public Error? Error { get; }

    public string Message { get; }

    private Upshot(bool isSuccess,
        string? message,
        Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
        Message = message ?? 
                  error?.Message ?? 
                  error?.Exception?.Message ??
                  string.Empty;
    }
    
    public static Upshot Success() 
        => new (true, string.Empty, null);
    
    public static Upshot Fail(string message, Exception exception)
        => new (false, message,  new (message, exception));

    public static Upshot Fail(Exception exception)
        => new (false, exception.Message, new (exception));

    public static Upshot Fail(string message)
        => new (false, message, null);

    public static Upshot Fail(Error? error)
        => new (false, error?.Message ?? string.Empty, error);
}

public readonly struct Upshot<T>
    : IUpshot<T> where T : new()
{
    public bool IsSuccess { get; }
    
    public bool IsFailure
        => !IsSuccess;

    public Error? Error { get; }

    public string Message { get; }

    public T Value { get; }

    private Upshot(bool isSuccess,
        string? message,
        Error? error,
        T value)
    {
        IsSuccess = isSuccess;
        Error = error;
        Value = value;
        Message = message ?? 
                  error?.Message ?? 
                  error?.Exception?.Message ??
                  string.Empty;
    }

    public static Upshot<T> Success(T value)
        => new (true, null, null, value);
    
    public static Upshot<T> Fail(Exception exception)
        => new (false, exception.Message, new (exception), default!);
    
    public static Upshot<T> Fail(string message)
        => new (false, message, null, default!);
    
    public static Upshot<T> Fail(Error? error)
        => new (false, error?.Message ?? string.Empty, error, default!);
}