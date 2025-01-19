using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

public static partial class UpshotExtensions
{
    public static Upshot OnSuccessRail(this Upshot upshot,
        Action action)
    {
        if (upshot.IsSuccess)
            action();

        return upshot;
    }
    
    public static Upshot OnFailRail(this Upshot upshot,
        Action action)

    {
        if (upshot.IsFailure)
            action();

        return upshot;
    }

    public static IUpshot StartRailWay(this Upshot upshot,
        Expression<Func<Upshot>> onSuccess, 
        Expression<Func<Upshot>> onFail)
    {
        try
        {
            return upshot.IsSuccess
                ? onSuccess.Compile().Invoke()
                : onFail.Compile().Invoke();
        }
        catch (Exception e)
        {
            return Upshot.Fail(e);
        }
    }
}