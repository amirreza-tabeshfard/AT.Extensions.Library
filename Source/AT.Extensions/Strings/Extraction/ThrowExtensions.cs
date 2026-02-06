using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class ThrowExtensions
{
    public static void ThrowIfEmpty(this String value, String paramName)
    {
        if (value.IsNullOrEmpty())
            throw new ArgumentException($"{paramName} cannot be empty", paramName);
    }

    internal static void ThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] this String? value, String paramName)
    {
        if (value.IsNull())
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null");
    }

    public static void ThrowIfNullEmptyOrWhitespace([System.Diagnostics.CodeAnalysis.NotNull] this String? value, String paramName)
    {
        value.ThrowIfNull(paramName);
        value.ThrowIfEmpty(paramName);
        value.ThrowIfWhitespace(paramName);
    }

    internal static void ThrowIfWhitespace(this String value, String paramName)
    {
        if (value.IsEmpty())
            return;

        if (value.IsWhitespace())
            throw new ArgumentException($"{paramName} cannot be whitespace only", paramName);
    }
}