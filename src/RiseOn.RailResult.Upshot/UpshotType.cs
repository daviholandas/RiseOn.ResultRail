namespace RiseOn.RailResult.Upshot;

/// <summary>
/// Represents the result of an operation, indicating success or failure, with a value.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public readonly struct Upshot<T> : IUpshot<T> where T : new()
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
    /// Gets the value associated with the operation.
    /// </summary>
    public T Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Upshot{T}"/> struct.
    /// </summary>
    /// <param name="isSuccess">Indicates whether the operation was successful.</param>
    /// <param name="error">The error associated with the operation, if any.</param>
    /// <param name="value">The value associated with the operation.</param>
    private Upshot(bool isSuccess, Error? error, T value)
    {
        IsSuccess = isSuccess;
        Error = error ?? default;
        Value = value;
    }

    /// <summary>
    /// Creates a successful <see cref="Upshot{T}"/> instance with a value.
    /// </summary>
    /// <param name="value">The value associated with the success.</param>
    /// <returns>A successful <see cref="Upshot{T}"/> instance.</returns>
    public static Upshot<T> Success(T value) => new(true, null, value);

    /// <summary>
    /// Creates a failed <see cref="Upshot{T}"/> instance with an exception.
    /// </summary>
    /// <param name="exception">The exception associated with the failure.</param>
    /// <returns>A failed <see cref="Upshot{T}"/> instance.</returns>
    public static Upshot<T> Fail(Exception exception) => new(false, exception, default!);

    /// <summary>
    /// Creates a failed <see cref="Upshot{T}"/> instance with a message.
    /// </summary>
    /// <param name="message">The failure message.</param>
    /// <returns>A failed <see cref="Upshot{T}"/> instance.</returns>
    public static Upshot<T> Fail(string message) => new(false, message, default!);

    /// <summary>
    /// Creates a failed <see cref="Upshot{T}"/> instance with an error.
    /// </summary>
    /// <param name="error">The error associated with the failure.</param>
    /// <returns>A failed <see cref="Upshot{T}"/> instance.</returns>
    public static Upshot<T> Fail(Error error) => new(false, error, default!);
}
