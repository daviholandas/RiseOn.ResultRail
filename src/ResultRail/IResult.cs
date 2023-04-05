namespace ResultRail;

public interface IResult
{
    bool IsSuccess { get; }
    
    bool IsFailure {get;}

    Error? Error { get; }

    string Message { get; }
}

public interface IResult<out T>
    : IResult where T : new()
{
    T Value { get; }
}