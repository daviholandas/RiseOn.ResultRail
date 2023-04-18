using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static Upshot<T> StartRailWay<T>(this T value,
        bool isSuccess = true,
        Error? error = null)
        where T : new()
        => isSuccess 
            ? Upshot<T>.Success(value)
            : Upshot<T>.Fail(error);

    public static Upshot<T> StartRailWay<T>(this T value,
        Func<T, bool> predicate,
        Error? error = null)
        where T : new()
        => predicate(value)
            ? Upshot<T>.Success(value)
            : Upshot<T>.Fail(error);
    

    public static Upshot<T> RailSuccess<T>(this Upshot<T> upshot,
        Action<T> action)
        where T : new()
    {
        if (upshot.IsSuccess)
             action(upshot.Value);

        return upshot;
    }
    
    public static Upshot<T> RailFail<T>(this Upshot<T> upshot,
        Action<T> action)
        where T : new()
    {
        if (upshot.IsFailure)
            action(upshot.Value);

        return upshot;
    }

    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot, 
        Func<T, TR> func) 
        where TR : new()
        where T : new()
    {
        return upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : Upshot<TR>.Success(func(upshot.Value));
    }

    public static Upshot<TR> Bind<T, TR>(this Upshot<T> upshot,
        Func<T, Upshot<TR>> func)
        where TR : new()
        where T : new()
    {
        return upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : func(upshot.Value);
    }
    
    public static TR Finally<T, TR>(this Upshot<T> result,
        Func<Upshot<T>, TR> func)
        where T : new()
        => func(result);
}