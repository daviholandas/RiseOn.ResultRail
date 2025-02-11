using System.Diagnostics;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static Upshot<T> OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, Upshot<T>> successRail,
        Func<T, Upshot<T>> failRail)
        where T : new()
        => predicate(value) ?
            successRail(value) :
            failRail(value);

    public static Upshot<TR> OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, Upshot<TR>> successRail,
        Func<T, Upshot<TR>> failRail)
        where T : new()
        where TR : new()
        => predicate(value) ?
            successRail(value) :
            failRail(value);

    public static T OnRail<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, T> successRail,
        Func<IUpshot<T>, T> failRail)
        where T : new()
        => upshot.IsSuccess ?
            successRail(upshot) :
            failRail(upshot);

    public static TR OnRail<T,TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> successRail,
        Func<IUpshot<T>, TR> failRail)
        where T : new()
        where TR : new()
        => upshot.IsSuccess ?
            successRail(upshot) :
            failRail(upshot);

    public static Upshot<T> OnRailSuccess<T>(this Upshot<T> upshot,
        Func<Upshot<T>, Upshot<T>> action)
        where T : new()
        => upshot.IsSuccess
            ? action(upshot)
            : upshot;

    public static IUpshot OnRailSuccess<T>(this Upshot<T> upshot,
        Func<Upshot<T>, IUpshot> action)
        where T : new()
        => upshot.IsSuccess
            ? action(upshot)
            : upshot;

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
        => upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : Upshot<TR>.Success(func(upshot.Value));

    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot,
        Func<T, Upshot<TR>> func)
        where TR : new()
        where T : new()
        => upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : func(upshot.Value);
    
    public static Upshot<TR> Map<T, TR>(this Upshot<T> upshot,
       Func<Upshot<T>, TR> func)
       where TR : new()
       where T : new()
       => upshot.IsFailure
            ? Upshot<TR>.Fail(upshot.Error)
            : Upshot<TR>.Success(func(upshot));

    public static TR Map<T, TR>(this Upshot<T> upshot,
        Func<T, TR> func,
        TR defaultValue)
        where TR : new()
        where T : new()
        => upshot.IsFailure
            ? defaultValue
            : func(upshot.Value);

    public static TR Finally<T, TR>(this Upshot<T> result,
        Func<Upshot<T>, TR> func)
        where T : new()
        => func(result);
}
