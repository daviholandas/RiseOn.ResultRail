namespace ResultRail.Extensions;

public static class SolutionExtensions
{
    public static Solution<T> ToPure<T>(this T value,
        bool isSuccess = true,
        Error? error = null)
        where T : new()
        => isSuccess 
            ? Solution<T>.Success(value)
            : Solution<T>.Fail(error);

    public static Solution<T> ToPure<T>(this T value,
        Func<T, bool> predicate,
        Error? error = null)
        where T : new()
        => predicate(value)
            ? Solution<T>.Success(value)
            : Solution<T>.Fail(error);
    

    public static Solution<T> RailSuccess<T>(this Solution<T> solution,
        Action<T> action)
        where T : new()
    {
        if (solution.IsSuccess)
             action(solution.Value);

        return solution;
    }
    
    public static Solution<T> RailFail<T>(this Solution<T> solution,
        Action<T> action)
        where T : new()
    {
        if (solution.IsFailure)
            action(solution.Value);

        return solution;
    }
    
    public static TR Finally<T, TR>(this Solution<T> result,
        Func<Solution<T>, TR> func)
        where T : new()
        => func(result);
    
    public static Solution<TR> Map<T, TR>(this Solution<T> result, Func<T, TR> func) 
        where TR : new()
        where T : new()
    {
        return result.IsFailure
            ? Solution<TR>.Fail(result.Error)
            : Solution<TR>.Success(func(result.Value));
    }
}