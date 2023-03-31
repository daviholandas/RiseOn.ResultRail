namespace ResultRail;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure
        => !IsSuccess;
    Error? Error { get; }
    string Message
        => Error?.Message ?? string.Empty;
}