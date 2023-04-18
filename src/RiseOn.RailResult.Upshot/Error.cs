using System.Text;

namespace RiseOn.RailResult.Upshot;

public readonly record struct Error(string? Message,
    Exception? Exception)
{
    public Error(string message)
        : this(message, null)
    {
        Message = message;
    }

    public Error(Exception? exception)
        : this(null, exception)
    {
        Exception = exception;
    }
    
    private bool PrintMembers(StringBuilder builder)
    {
        if (Exception is not null)
        {
            builder.Append("Exception = ");
            builder.Append(" { ");
            builder.Append($" Type = { Exception.GetType().Name }, ");
            builder.Append($" Message = { Exception.Message }, ");
            builder.Append($" InnerException = { Exception.InnerException?.Message }");
            builder.Append(" }");
        }

        if(Exception is not null && Message is not null)
            builder.Append($", ");

        if(Message is not null)
            builder.Append($"Message = {Message}");

        return true;
    }
};