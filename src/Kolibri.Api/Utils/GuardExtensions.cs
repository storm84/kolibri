using System.Runtime.CompilerServices;

namespace Kolibri.Api.Utils;

public static class GuardExtensions
{
    public static T ThrowIfNull<T>(
        this T input,
        [CallerArgumentExpression("input")]
        string? inputName = default)
    {
        return input ?? throw new ArgumentNullException(inputName);
    }

    public static T ThrowIfNull<T>(
        this T? input,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : struct
    {
        return input ?? throw new ArgumentNullException(inputName);
    }

    public static T ThrowIfDefault<T>(
        this T input,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : struct
    {
        if (input.Equals(default(T)))
            throw new ArgumentException("Argument must not be default", inputName);
        return input;
    }

    public static string ThrowIfNullOrEmpty(
        this string input,
        [CallerArgumentExpression("input")]
        string? inputName = default)
    {
        return string.IsNullOrEmpty(input)
            ? throw new ArgumentException("Argument string must not be null or empty", inputName)
            : input;
    }

    public static string ThrowIfNullOrWhiteSpace(
        this string input,
        [CallerArgumentExpression("input")]
        string? inputName = default)
    {
        return string.IsNullOrWhiteSpace(input)
            ? throw new ArgumentException("Argument string must not be null or empty", inputName)
            : input;
    }

    public static T ThrowIfLessThan<T>(
        this T input,
        T value,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : IComparable
    {
        if (input.CompareTo(value) < 0)
            throw new ArgumentException($"Argument must not be less than {value}", inputName);
        return input;
    }

    public static T ThrowIfGraterThan<T>(
        this T input,
        T value,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : IComparable
    {
        if (input.CompareTo(value) > 0)
            throw new ArgumentException($"Argument must not be grater than {value}", inputName);
        return input;
    }

    public static T ThrowIfEqualTo<T>(
        this T input,
        T value,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : IComparable
    {
        if (input.CompareTo(value) == 0)
            throw new ArgumentException($"Argument must not be equal to {value}", inputName);
        return input;
    }

    public static T ThrowIfNotEqualTo<T>(
        this T input,
        T value,
        [CallerArgumentExpression("input")]
        string? inputName = default)
        where T : IComparable
    {
        if (input.CompareTo(value) != 0)
            throw new ArgumentException($"Argument must be equal to {value}", inputName);
        return input;
    }
}