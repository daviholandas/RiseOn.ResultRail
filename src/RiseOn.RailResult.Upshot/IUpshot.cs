namespace RiseOn.RailResult.Upshot;

public interface IUpshot
{
    bool IsSuccess { get; }
    
    bool IsFailure {get;}

    Error? Error { get; }

    string Message { get; }
}

public interface IUpshot<out T>
    : IUpshot where T : new()
{
    T Value { get; }
}