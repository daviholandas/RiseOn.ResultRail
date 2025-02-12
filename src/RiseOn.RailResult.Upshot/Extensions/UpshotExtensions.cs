using System.Linq.Expressions;

namespace RiseOn.RailResult.Upshot.Extensions;

/// <summary>
/// Provides extension methods for handling IUpshot instances.
/// </summary>
public static partial class UpshotExtensions
{
    /// <summary>
    /// Executes the appropriate rail based on the success status of the upshot.
    /// </summary>
    /// <param name="upshot">The upshot instance.</param>
    /// <param name="successRail">The function to execute if the upshot is successful.</param>
    /// <param name="failRail">The function to execute if the upshot is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public static IUpshot OnRail(this IUpshot upshot,
        Func<IUpshot> successRail,
        Func<IUpshot> failRail)
        => upshot.IsSuccess ? successRail() : failRail();

    /// <summary>
    /// Executes the appropriate rail based on the predicate result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="predicate">The predicate to determine which rail to execute.</param>
    /// <param name="successRail">The function to execute if the predicate is true.</param>
    /// <param name="failRail">The function to execute if the predicate is false.</param>
    /// <returns>The result of the executed function.</returns>
    public static TR OnRail<T, TR>(this T value,
        Func<T, bool> predicate,
        Func<T, TR> successRail,
        Func<T, TR> failRail)
        where T : new()
        where TR : new()
        => predicate(value) ? successRail(value) : failRail(value);

    /// <summary>
    /// Executes the appropriate rail based on the predicate result.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to evaluate.</param>
    /// <param name="predicate">The predicate to determine which rail to execute.</param>
    /// <param name="successRail">The function to execute if the predicate is true.</param>
    /// <param name="failRail">The function to execute if the predicate is false.</param>
    /// <returns>The result of the executed function.</returns>
    public static IUpshot OnRail<T>(this T value,
        Func<T, bool> predicate,
        Func<T, IUpshot> successRail,
        Func<T, IUpshot> failRail)
        where T : new()
        => predicate(value) ? successRail(value) : failRail(value);

    /// <summary>
    /// Executes the action if the upshot is successful.
    /// </summary>
    /// <param name="upshot">The upshot instance.</param>
    /// <param name="action">The action to execute if the upshot is successful.</param>
    /// <returns>The result of the executed action or the original upshot.</returns>
    public static IUpshot OnRailSuccess(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsSuccess ? action() : upshot;

    /// <summary>
    /// Executes the action if the upshot is a failure.
    /// </summary>
    /// <param name="upshot">The upshot instance.</param>
    /// <param name="action">The action to execute if the upshot is a failure.</param>
    /// <returns>The result of the executed action or the original upshot.</returns>
    public static IUpshot OnRailFail(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsFailure ? action() : upshot;

    /// <summary>
    /// Maps the upshot to a new value if it is successful, otherwise returns the default value.
    /// </summary>
    /// <typeparam name="TR">The type of the result.</typeparam>
    /// <param name="upshot">The upshot instance.</param>
    /// <param name="defaultValue">The default value to return if the upshot is a failure.</param>
    /// <param name="func">The function to map the upshot to a new value.</param>
    /// <returns>The mapped value or the default value.</returns>
    public static TR? Map<TR>(this IUpshot upshot,
        TR? defaultValue,
        Func<IUpshot, TR> func)
        => upshot.IsSuccess ? func(upshot) : defaultValue ?? default;

    /// <summary>
    /// Maps the upshot to a new upshot if it is successful, otherwise returns a failed upshot.
    /// </summary>
    /// <param name="upshot">The upshot instance.</param>
    /// <param name="action">The function to map the upshot to a new upshot.</param>
    /// <returns>The mapped upshot or a failed upshot.</returns>
    public static IUpshot Map(this IUpshot upshot,
        Func<IUpshot> action)
        => upshot.IsSuccess ? action() : Upshot.Fail(upshot.Error);
}
