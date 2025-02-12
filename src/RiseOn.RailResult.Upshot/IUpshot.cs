namespace RiseOn.RailResult.Upshot;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public interface IUpshot
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    bool IsFailure { get; }

    /// <summary>
    /// Gets the error information if the operation failed.
    /// </summary>
    Error Error { get; }
}

/// <summary>
/// Represents the result of an operation with a value, indicating success or failure.
/// </summary>
/// <typeparam name="T">The type of the value returned by the operation.</typeparam>
public interface IUpshot<out T> : IUpshot where T : new()
{
    /// <summary>
    /// Gets the value returned by the operation.
    /// </summary>
    T Value { get; }
}