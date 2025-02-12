namespace RiseOn.RailResult.Upshot.Extensions;

/// <summary>
/// Provides extension methods for handling IUpshot types.
/// </summary>
public static partial class UpshotExtensions
{
    /// <summary>
    /// Executes the appropriate function based on the predicate result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="predicate">The predicate to evaluate the value.</param>
    /// <param name="successRail">The function to execute if the predicate is true.</param>
    /// <param name="failRail">The function to execute if the predicate is false.</param>
    /// <returns>The result of the executed function.</returns>
    public static IUpshot<T> OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot<T>> successRail,
        Func<T, IUpshot<T>> failRail)
        where T : new()
        => predicate(value) ? successRail(value) : failRail(value);

    /// <summary>
    /// Executes the appropriate function based on the predicate result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="predicate">The predicate to evaluate the value.</param>
    /// <param name="successRail">The function to execute if the predicate is true.</param>
    /// <param name="failRail">The function to execute if the predicate is false.</param>
    /// <returns>The result of the executed function.</returns>
    public static IUpshot<TR> OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot<TR>> successRail,
        Func<T, IUpshot<TR>> failRail)
        where T : new()
        where TR : new()
        => predicate(value) ? successRail(value) : failRail(value);

    /// <summary>
    /// Executes the appropriate function based on the success state of the upshot.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="successRail">The function to execute if the upshot is successful.</param>
    /// <param name="failRail">The function to execute if the upshot is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public static T OnRail<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, T> successRail,
        Func<IUpshot<T>, T> failRail)
        where T : new()
        => upshot.IsSuccess ? successRail(upshot) : failRail(upshot);

    /// <summary>
    /// Executes the appropriate function based on the success state of the upshot.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="successRail">The function to execute if the upshot is successful.</param>
    /// <param name="failRail">The function to execute if the upshot is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public static TR OnRail<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> successRail,
        Func<IUpshot<T>, TR> failRail)
        where T : new()
        where TR : new()
        => upshot.IsSuccess ? successRail(upshot) : failRail(upshot);

    /// <summary>
    /// Executes the specified action if the upshot is successful.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The original upshot or the result of the action.</returns>
    public static IUpshot<T> OnRailSuccess<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot<T>> action)
        where T : new()
        => upshot.IsSuccess ? action(upshot) : upshot;

    /// <summary>
    /// Executes the specified action if the upshot is successful.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The original upshot or the result of the action.</returns>
    public static IUpshot OnRailSuccess<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot> action)
        where T : new()
        => upshot.IsSuccess ? action(upshot) : upshot;

    /// <summary>
    /// Maps the upshot to a new result if the upshot is successful.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    /// <summary>
    /// Maps the upshot to a new result if the upshot is successful.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    /// <summary>
    /// Maps the upshot to a new result if the upshot is successful.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailSuccess<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.Map(action);

    /// <summary>
    /// Executes the specified action if the upshot is a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is a failure.</param>
    /// <returns>The original upshot or the result of the action.</returns>
    public static IUpshot<T> OnRailFail<T>(this IUpshot<T> upshot,
        Func<IUpshot<T>, IUpshot<T>> action)
        where T : new()
        => upshot.IsFailure ? action(upshot) : upshot;

    /// <summary>
    /// Maps the upshot to a new result if the upshot is a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is a failure.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? Upshot<TR>.Success(action(upshot)) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result if the upshot is a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is a failure.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? action(upshot.Value) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result if the upshot is a failure.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="action">The action to execute if the upshot is a failure.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> OnRailFail<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> action)
        where T : new()
        where TR : new()
        => upshot.IsFailure ? Upshot<TR>.Success(action(upshot.Value)) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="func">The function to map the value.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? Upshot<TR>.Success(func(upshot.Value)) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="func">The function to map the value.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<T, IUpshot<TR>> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? func(upshot.Value) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="func">The function to map the value.</param>
    /// <returns>The mapped result.</returns>
    public static IUpshot<TR> Map<T, TR>(this IUpshot<T> upshot,
        Func<IUpshot<T>, TR> func)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? Upshot<TR>.Success(func(upshot)) : Upshot<TR>.Fail(upshot.Error);

    /// <summary>
    /// Maps the upshot to a new result or returns a default value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot to evaluate.</param>
    /// <param name="func">The function to map the value.</param>
    /// <param name="defaultValue">The default value to return if the upshot is not successful.</param>
    /// <returns>The mapped result or the default value.</returns>
    public static TR? Map<T, TR>(this IUpshot<T> upshot,
        Func<T, TR> func,
        TR? defaultValue)
        where TR : new()
        where T : new()
        => upshot.IsSuccess ? defaultValue ?? default : func(upshot.Value);

    /// <summary>
    /// Executes the specified function and returns its result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="result">The upshot to evaluate.</param>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result of the executed function.</returns>
    public static TR Finally<T, TR>(this IUpshot<T> result,
        Func<IUpshot<T>, TR> func)
        where T : new()
        => func(result);
}
