namespace AT.Extensions.Strings.Extraction;
public static partial class KeepExtensions
{
    #region Method(s): Private

    [System.Text.RegularExpressions.GeneratedRegex("[^0-9]")]
    private static partial System.Text.RegularExpressions.Regex DigitsOnly();

    [System.Text.RegularExpressions.GeneratedRegex("[^0a-zA-Z]")]
    private static partial System.Text.RegularExpressions.Regex StripNonLetters();

    [System.Text.RegularExpressions.GeneratedRegex("[^0-9a-zA-Z]")]
    private static partial System.Text.RegularExpressions.Regex StripNonAlphanumeric();

    [System.Text.RegularExpressions.GeneratedRegex("[^\\w]*[0-9_]*")]
    private static partial System.Text.RegularExpressions.Regex StripNonAlphaNumeric();

    [System.Text.RegularExpressions.GeneratedRegex("[^\\w]*[_]*")]
    private static partial System.Text.RegularExpressions.Regex StripNonWordChars();

    #endregion

    public static String KeepDigitsOnly(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return DigitsOnly().Replace(value, String.Empty);
    }

    public static String KeepLettersOnly(this String value, bool withAccentedLetters = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (withAccentedLetters)
            return StripNonAlphaNumeric().Replace(value, String.Empty);
        else
            return StripNonLetters().Replace(value, String.Empty);
    }

    public static String KeepLettersOrDigitsOnly(this String value, bool withAccentedLetters = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (withAccentedLetters)
            return StripNonWordChars().Replace(value, String.Empty);
        else
            return StripNonAlphanumeric().Replace(value, String.Empty);
    }
}