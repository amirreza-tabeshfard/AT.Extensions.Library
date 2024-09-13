namespace AT.Extensions.Strings.Extraction;
public static class RegexExtensions : Object
{
    #region Field(s)

    private static readonly System.Text.RegularExpressions.RegexOptions InvariantCultureIgnoreCaseRegexOptions = System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant;

    #endregion

    public static String RegexReplace(this String input, String pattern, Func<String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(replaceCallback);
        // ----------------------------------------------------------------------------------------------------
        return input.RegexReplace
        (
            pattern,
            InvariantCultureIgnoreCaseRegexOptions,
            replaceCallback
        );
    }

    public static String RegexReplace(this String input, String pattern, Func<Int32, String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(replaceCallback);
        // ----------------------------------------------------------------------------------------------------
        return input.RegexReplace
        (
            pattern,
            InvariantCultureIgnoreCaseRegexOptions,
            replaceCallback
        );
    }

    public static String RegexReplace(this String input, String pattern, System.Text.RegularExpressions.RegexOptions options, Func<String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(replaceCallback);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? String.Empty,
            me => replaceCallback(me.Value),
            options
        );
    }

    public static String RegexReplace(this String input, String pattern, System.Text.RegularExpressions.RegexOptions options, Func<Int32, String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentNullException.ThrowIfNull(replaceCallback);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? String.Empty,
            me => replaceCallback(me.Index, me.Value),
            options
        );
    }

    public static String RegexReplace(this String input, String pattern, String newValue, System.Text.RegularExpressions.RegexOptions? options = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        ArgumentException.ThrowIfNullOrEmpty(newValue);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? String.Empty,
            newValue ?? String.Empty,
            options ?? InvariantCultureIgnoreCaseRegexOptions
        );
    }
}