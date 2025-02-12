using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static IUpshot OnRail(this IUpshot upshot,
        Func<IUpshot> successRail,
        Func<IUpshot> failRail)
        => upshot.IsSuccess ? successRail() : failRail();

    public static TR OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, TR> successRail,
        Func<T, TR> failRail)
        where T : new()
        where TR: new()
        => predicate(value) ? successRail(value) : failRail(value);

    public static IUpshot OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot> successRail,
        Func<T, IUpshot> failRail)
        where T : new()
        => predicate(value) ? successRail(value) : failRail(value);

    public static IUpshot OnRailSuccess(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsSuccess ? action() : upshot;

    public static IUpshot OnRailFail(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsFailure ? action() : upshot;

    public static TR? Map<TR>(this IUpshot upshot,
        TR? defaultValue,
        Func<IUpshot, TR> func)
        => upshot.IsSuccess ? func(upshot) : defaultValue ?? default;

    public static IUpshot Map(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsSuccess ? action() : Upshot.Fail(upshot.Error);
}