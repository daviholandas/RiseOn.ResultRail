using System.Diagnostics;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static IUpshot<T> OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot<T>> successRail,
        Func<T, IUpshot<T>> failRail)
        where T : new()
        => predicate(value) ? successRail(value) : failRail(value);

    public static IUpshot<TR> OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot<TR>> successRail,
        Func<T, IUpshot<TR>> failRail)
        where T : new()
        where TR : new()
        => predicate(value) ? successRail(value) : failRail(value);

    public static T OnRail<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, T> successRail,
        Func<IUpshot<T>, T> failRail)
        where T : new()
        => upshot.IsSuccess ? successRail(upshot) : failRail(upshot);

    public static TR OnRail<T,TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> successRail,
        Func<IUpshot<T>, TR> failRail)
        where T : new()
        where TR : new()
        => upshot.IsSuccess ? successRail(upshot) : failRail(upshot);

    public static IUpshot<T> OnRailSuccess<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot<T>> action)
        where T : new()
        => upshot.IsSuccess ? action(upshot) : upshot;

    public static IUpshot OnRailSuccess<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot> action)
        where T : new()
        => upshot.IsSuccess ? action(upshot) : upshot;

    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    public static IUpshot<T> OnRailFail<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot<T>> action)
        where T : new()
        => upshot.IsFailure ? action(upshot) : upshot;

    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? Upshot<TR>.Success(action(upshot)) : Upshot<TR>.Fail(upshot.Error);

    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? action(upshot.Value) : Upshot<TR>.Fail(upshot.Error);

    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? Upshot<TR>.Success(action(upshot.Value)) : Upshot<TR>.Fail(upshot.Error);

    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? Upshot<TR>.Success(func(upshot.Value)) : Upshot<TR>.Fail(upshot.Error);

    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? func(upshot.Value) : Upshot<TR>.Fail(upshot.Error);

    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? Upshot<TR>.Success(func(upshot)) : Upshot<TR>.Fail(upshot.Error);

    public static TR? Map<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> func,
        TR? defaultValue)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? defaultValue ?? default : func(upshot.Value);

    public static TR Finally<T, TR>(this IUpshot<T> result,
        Func<IUpshot<T>, TR> func)
        where T : new()
        => func(result);
}
