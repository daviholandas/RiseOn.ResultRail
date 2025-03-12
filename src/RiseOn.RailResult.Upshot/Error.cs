using System.Text;

namespace RiseOn.RailResult.Upshot;

public sealed class Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> struct with a specified exception.
    /// </summary>
    /// <param name="exception">The exception associated with the error.</param>
    /// <param name="message">The message associated with the error, if not informed of the message of error,  the exception message will be used.</param>
    private Error(Exception? exception,
        string message)
    {
        Exception = exception;
        Message = message;
    }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string? Message { get; }
    /// <summary>
    /// Gets the exception associated with the error.
    /// </summary>
    public Exception? Exception { get; }

    public static implicit operator Error(string message) => new(null, message);
    public static implicit operator Error(Exception exception) => new(exception, exception.Message);
    public static explicit operator string(Error error) => error.ToString();
    public static explicit operator Exception(Error error) => error.Exception ?? new Exception(error.Message);

    private bool PrintMembers(StringBuilder builder)
    {
        ToString(builder);
        return true;
    }

    public override string ToString()
        => ToString(new StringBuilder());

    private string ToString(StringBuilder builder)
    {
        if (Exception is not null)
        {
            builder.Append("Exception = ")
                .AppendLine("{ ")
                    .Append("       Type = ").Append(Exception.GetType().Name).AppendLine(", ")
                    .Append("       Message = ").Append(Exception.Message).AppendLine(", ");
            if (Exception.InnerException is not null)
                builder.Append("        InnerException = ").Append(Exception.InnerException.Message).AppendLine(", ");
            if (Exception.StackTrace is not null)
                builder.Append("        StackTrace = ").Append(Exception.StackTrace);

            builder.Append(" }");

            return builder.ToString();
        }

        builder.Append("Message = ").Append(Message);

        return builder.ToString();
    }
};