using Humanizer;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
///     String extensions for naming conventions.
/// </summary>
/// <remarks>
///     This class contains extension methods for strings.
///     It includes methods to convert strings to snake case and pluralize them.
/// </remarks>
public static class StringExtensions
{
    /// <summary>
    ///     Converts a string to snake_case.
    /// </summary>
    /// <param name="text">The input string</param>
    /// <returns>snake_case formatted string</returns>
    public static string ToSnakeCase(this string text)
    {
        return new string(Convert(text.GetEnumerator()).ToArray());

        static IEnumerable<char> Convert(CharEnumerator e)
        {
            if (!e.MoveNext()) yield break;

            yield return char.ToLower(e.Current);

            while (e.MoveNext())
            {
                if (char.IsUpper(e.Current))
                {
                    yield return '_';
                    yield return char.ToLower(e.Current);
                }
                else
                {
                    yield return e.Current;
                }
            }
        }
    }

    /// <summary>
    ///     Pluralizes a string using Humanizer.
    /// </summary>
    /// <param name="text">The input string</param>
    /// <returns>Pluralized string</returns>
    public static string ToPlural(this string text)
    {
        return text.Pluralize(false);
    }
}