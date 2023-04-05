namespace ResultRail;

public readonly struct Solution
    : IResult
{
    public  bool IsSuccess { get; }
    
    public bool IsFailure
        => !IsSuccess;

    public Error? Error { get; }

    public string Message { get; }

    private Solution(bool isSuccess,
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
    
    public static Solution Success() 
        => new (true, null, null);
    
    public static Solution Fail(string message, Exception exception)
        => new (false, message,  new (message, exception));

    public static Solution Fail(Exception exception)
        => new (false, null, new (exception));
    
    public static Solution Fail(string message)
        => new (false, message, null);
}

public readonly struct Solution<T>
    : IResult<T> where T : new()
{
    public bool IsSuccess { get; }
    
    public bool IsFailure
        => !IsSuccess;

    public Error? Error { get; }

    public string Message { get; }

    public T Value { get; }

    private Solution(bool isSuccess,
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

    public static Solution<T> Success(T value)
        => new (true, null, null, value);
    
    public static Solution<T> Fail(Exception exception)
        => new (false, null, new (exception), default!);
    
    public static Solution<T> Fail(string message)
        => new (false, message, null, default!);
    
    public static Solution<T> Fail(Error? error)
        => new (false, error?.Message, error, default!);
    
    public static Solution<T> RailWay(Func<Solution<T>> predicate)
    {
        var predicateResult = predicate();

        if (predicateResult.IsFailure)
            return predicateResult.Error is not null
                ? Fail(predicateResult.Error)
                : Fail(predicateResult.Message);

        return predicateResult;
    }
}