using System.Text;

namespace RiseOn.RailResult.Upshot;

public readonly record struct Error(string? Message,
    Exception? Exception)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> struct with a specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    private Error(string message)
        : this(message, null)
    {
        Message = message;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> struct with a specified exception.
    /// </summary>
    /// <param name="exception">The exception associated with the error.</param>
    private Error(Exception? exception)
        : this(null, exception)
    {
        Exception = exception;
        Message = exception?.Message;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string? Message { get; }
    /// <summary>
    /// Gets the exception associated with the error.
    /// </summary>
    public Exception? Exception { get; }

    public static implicit operator Error(string message) => new(message);
    public static implicit operator Error(Exception exception) => new(exception);
    public static explicit operator string(Error error) => error.Message ?? error.Exception?.Message ?? string.Empty;
    public static explicit operator Exception(Error error) => error.Exception ?? new Exception(error.Message);


    /// <summary>
    /// Appends the string representation of the error to the specified <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="StringBuilder"/> to append to.</param>
    /// <returns>true if the members were appended; otherwise, false.</returns>
    private bool PrintMembers(StringBuilder builder)
    {
        if (Exception is not null)
        {
            builder.Append("Exception = { ");
            builder.Append("Type = ").Append(Exception.GetType().Name).Append(", ");
            builder.Append("Message = ").Append(Exception.Message).Append(", ");
            if (Exception.InnerException is not null)
                builder.Append("InnerException = ").Append(Exception.InnerException.Message).Append(", ");
            builder.Append("StackTrace = ").Append(Exception.StackTrace).Append(" }");
        }

        if (Exception is not null && Message is not null)
            builder.Append(", ");

        if (Message is not null)
            builder.Append("Message = ").Append(Message);

        return true;
    }
};