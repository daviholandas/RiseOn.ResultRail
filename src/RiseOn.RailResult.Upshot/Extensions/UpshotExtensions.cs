using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
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
}