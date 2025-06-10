using System.Text.RegularExpressions;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     String extension methods for formatting purposes
/// </summary>
/// <remarks>
///     Provides a method to convert a string from PascalCase or camelCase to kebab-case.
///     Uses a compiled regular expression for efficient transformation.
/// </remarks>
public static partial class StringExtensions
{
    /// <summary>
    ///     Converts a string to kebab-case using a regex-based approach.
    /// </summary>
    /// <param name="text">The input string to convert</param>
    /// <returns>A kebab-case version of the input string</returns>
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return text;
        }
        
        return KebabCaseRegex().Replace(text, "-$1")
            .Trim()
            .ToLower();
    }

    /// <summary>
    ///     Precompiled regex to match camelCase or PascalCase word boundaries.
    /// </summary>
    /// <returns>A compiled Regex instance for kebab-case conversion</returns>
    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}