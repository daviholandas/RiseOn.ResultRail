using System.Diagnostics;

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

    public static Upshot<TR> OnRail<T, TR>(this Upshot<T> upshot,
        Func<Upshot<T>, Upshot<TR>> action)
        where T : new()
        where TR : new()
        => action(upshot);

    public static Upshot<TR> OnRail<T, TR>(this T obj,
        Func<T, Upshot<TR>> action)
        where T : new()
        where TR : new()
        => action(obj);


    public static Upshot<T> OnRailSuccess<T>(this Upshot<T> upshot,
        Func<Upshot<T>, Upshot<T>> action)
        where T : new()
    {
        if (upshot.IsSuccess)
            return action(upshot);

        return upshot;
    }

    public static Upshot<TR> OnRailSuccess<T, TR>(this Upshot<T> upshot,
        Func<Upshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static Upshot<TR> OnRailSuccess<T, TR>(this Upshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static Upshot<TR> OnRailSuccess<T, TR>(this Upshot<T> upshot,
        Func<T, Upshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);


    public static Upshot<T> OnRailFail<T>(this Upshot<T> upshot,
        Func<Upshot<T>, Upshot<T>> action)
        where T : new()
        => upshot.IsFailure
            ? action(upshot)
            : upshot;

    public static Upshot<TR> OnRailFail<T, TR>(this Upshot<T> upshot,
        Func<Upshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static Upshot<TR> OnRailFail<T, TR>(this Upshot<T> upshot,
        Func<T, Upshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static Upshot<TR> OnRailFail<T, TR>(this Upshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot,
        Func<T, TR> func)
        where TR : new()
        where T : new()
    {
        return upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : Upshot<TR>.Success(func(upshot.Value));
    }

    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot,
        Func<T, Upshot<TR>> func)
        where TR : new()
        where T : new()
    {
        return upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : func(upshot.Value);
    }

    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot,
       Func<Upshot<T>, TR> func)
       where TR : new()
       where T : new()
    {
        return upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : Upshot<TR>.Success(func(upshot));
    }

    public static TR Map<T, TR>(this Upshot<T> upshot,
        Func<T, TR> func,
        TR defaultValue)
        where TR : new()
        where T : new()
    {
        return upshot.IsFailure
            ? defaultValue
            : func(upshot.Value);
    }


    public static TR Finally<T, TR>(this Upshot<T> result,
        Func<Upshot<T>, TR> func)
        where T : new()
        => func(result);
}
