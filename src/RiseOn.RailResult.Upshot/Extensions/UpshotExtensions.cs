using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static IUpshot OnRail(this IUpshot upshot,
        Func<IUpshot> successRail,
        Func<IUpshot> failRail)
        => upshot.IsSuccess ?
            successRail() :
            failRail();

    public static TR OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, TR> successRail,
        Func<T, TR> failRail)
        where T : new()
        where TR: new()
        => predicate(value) ?
            successRail(value) :
            failRail(value);

    public static IUpshot OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot> successRail,
        Func<T, IUpshot> failRail)
        where T : new()
        => predicate(value) ?
            successRail(value) :
            failRail(value);

    public static Upshot OnRailSuccess(this Upshot upshot,
        Action<Upshot> action)
    {
        if (upshot.IsSuccess)
            action(upshot);

        return upshot;
    }
    
    public static Upshot OnRailFail(this Upshot upshot,
        Action<Upshot> action)
    {
        if (upshot.IsFailure)
            action(upshot);

        return upshot;
    }

    public static Upshot OnRail(this Upshot upshot,
        Expression<Func<Upshot, Upshot>> expression)
    {
        return upshot.IsSuccess
            ? expression.Compile().Invoke(upshot)
            : upshot;
        //TODO: It's wrong. It doing the same thing of OnRailSuccess
    }

    public static TR Map<TR>(this Upshot upshot,
        Func<Upshot, TR> func)
    {
        return func(upshot);
    }

    public static Upshot<TR> Map<TR>(this Upshot upshot,
        Func<TR> func)
        where TR : new ()
    {
        if(upshot.IsFailure)
            return Upshot<TR>.Fail(upshot.Message);

        return Upshot<TR>.Success(func());
    }
}