namespace RiseOn.RailResult.Upshot;

/// <summary>
/// Represents the result of an operation, indicating success or failure.
/// </summary>
public readonly struct Upshot : IUpshot
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error associated with the operation, if any.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Upshot"/> struct.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error associated with the operation, if any.</param>
    private Upshot(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error ?? default;
    }

    /// <summary>
    /// Creates a successful <see cref="Upshot"/> instance.
    /// </summary>
    /// <returns>A successful <see cref="Upshot"/> instance.</returns>
    public static Upshot Success() => new(true, null);

    /// <summary>
    /// Creates a failed <see cref="Upshot"/> instance with a message and an exception.
    /// </summary>
    /// <param name="errorMessage">The failure message.</param>
    /// <returns>A failed <see cref="Upshot"/> instance.</returns>
    public static Upshot Fail(string errorMessage) => new(false, errorMessage);

    /// <summary>
    /// Creates a failed <see cref="Upshot"/> instance with an exception.
    /// </summary>
    /// <param name="exception">The exception associated with the failure.</param>
    /// <returns>A failed <see cref="Upshot"/> instance.</returns>
    public static Upshot Fail(Exception exception) => new(false, exception);

    /// <summary>
    /// Creates a failed <see cref="Upshot"/> instance with an error.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <returns>A failed <see cref="Upshot"/> instance.</returns>
    public static Upshot Fail(Error error) => new(false, error);
}
