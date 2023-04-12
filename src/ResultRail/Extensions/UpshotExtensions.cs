using System.Linq.Expressions;

namespace ResultRail.Extensions;

public static partial class UpshotExtensions
{
    public static Upshot RailSuccess<T>(this Upshot upshot,
        Action action)
        where T : new()
    {
        if (upshot.IsSuccess)
            action();

        return upshot;
    }
    
    public static Upshot RailFail<T>(this Upshot upshot,
        Action action)
        where T : new()
    {
        if (upshot.IsFailure)
            action();

        return upshot;
    }
}