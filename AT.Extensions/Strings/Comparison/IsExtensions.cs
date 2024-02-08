using AT.Extensions.Chars.Comparison;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Strings.Comparison;
public static partial class IsExtensions : Object
{
    #region Method(s): Private

    private static bool PassesSanityCheckForCases(this String value)
    {
        return !value.Contains(' ');
    }

    #endregion

    #region Method(s): Private Regex Attributes (It doesn't work in .NET 6 version)

    [System.Text.RegularExpressions.GeneratedRegex("^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$")]
    private static partial System.Text.RegularExpressions.Regex EmailAddress();

    [System.Text.RegularExpressions.GeneratedRegex("(?:^|\\s)([a-z]{3,6}(?=://))?(://)?((?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?)\\.(?:25[0-5]|2[0-4]\\d|[01]?\\d\\d?))(?::(\\d{2,5}))?(?:\\s|$)")]
    private static partial System.Text.RegularExpressions.Regex IPv4();

    #endregion

    public static bool IsAllDecimalDigits(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        SharperHacks.CoreLibs.Constraints.Verify.IsNotNull(value);
        return value.IsLimitedToSetOf(SharperHacks.CoreLibs.Constants.StandardSets.DecimalDigits);
    }

    public static bool IsAllDigits(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        foreach (char c in value)
            if (c < '0' || c > '9')
                return default;
        // ----------------------------------------------------------------------------------------------------
        return true;
    }

    public static bool IsAllHexDigits(this String value, bool prefixRequired = default)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        SharperHacks.CoreLibs.Constraints.Verify.IsNotNull(value);
        // ----------------------------------------------------------------------------------------------------
        return value.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)
               ? value[2..].IsLimitedToSetOf(SharperHacks.CoreLibs.Constants.StandardSets.HexDigits)
               : !prefixRequired && value.IsLimitedToSetOf(SharperHacks.CoreLibs.Constants.StandardSets.HexDigits);
    }

    public static bool IsCamelCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter()) && value.FirstOrDefault().IsLower();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static bool IsDateTime(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParse(value, out DateTime _);
    }

    public static bool IsDateTime(this String value, string format)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (String.IsNullOrEmpty(format))
            throw new ArgumentNullException(nameof(format));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime _);
    }

    public static bool IsEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(false)] this String value)
    {
        return (string.IsNullOrWhiteSpace(value)) && (value.Length == 0);
    }

    public static bool IsEmptyOrWhiteSpace(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        bool result;
        if (value.IsEmpty())
            result = true;
        else
        {
            result = true;
            foreach (char character in value)
                if (!Char.IsWhiteSpace(character))
                {
                    result = false;
                    break;
                }
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsEndOfSentenceCharacter(string value, int pos)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        char c = value[pos];
        return c == '!' ||
               c == '?' ||
               c == ':' ||
               (c == '.' && !(pos < (value.Length - 1) && char.IsDigit(value[pos + 1])));
    }

    public static bool IsEndOfSentenceCharacter(System.Text.StringBuilder builder, int pos)
    {
        if (builder == default)
            throw new ArgumentNullException(nameof(builder));
        // ----------------------------------------------------------------------------------------------------
        char c = builder[pos];
        return c == '!' ||
               c == '?' ||
               c == ':' ||
               (c == '.' && !(pos < (builder.Length - 1) && char.IsDigit(builder[pos + 1])));
    }

    public static bool IsHungarianNotation(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter() || x == '_') && !value.ContainsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static bool IsKebabCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter() || x == '-') && !value.ContainsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static bool IsLengthBetween(this String value, int minimum, int maximum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsMinLength(minimum)
               && value.IsMaxLength(maximum);
    }

    public static bool IsLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Trim().Replace(oldValue: " ", newValue: String.Empty).All(Char.IsLetter);
    }

    public static bool IsLetterOrDigit(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Trim().Replace(oldValue: " ", newValue: String.Empty).All(Char.IsLetterOrDigit);
    }

    public static bool IsLimitedToSetOf(this String value, System.Collections.Immutable.ImmutableHashSet<char> set)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        SharperHacks.CoreLibs.Constraints.Verify.IsNotNull(value);

        if (value.IsEmpty())
            return set.Count == 0;

        foreach (char ch in value)
            if (!set.Contains(ch))
                return false;
        // ----------------------------------------------------------------------------------------------------
        return true;
    }

    public static bool IsMaxLength(this String value, int maximum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length <= maximum;
    }

    public static bool IsMinLength(this String value, int minimum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length >= minimum;
    }

    public static bool IsNotNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this string? value)
    {
        return !String.IsNullOrEmpty(value);
    }

    public static void IsNotNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this string? value, Action<string> action)
    {
        if (value.IsNotNullOrEmpty())
            action(value);
    }

    public static bool IsNotNullOrWhiteSpace(this String value)
    {
        return !String.IsNullOrWhiteSpace(value);
    }

    public static bool IsNull(this String value)
    {
        return (value is null) || (value == default);
    }

    public static bool IsNullEmptyOrWhitespace([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this String value)
    {
        return value.IsNull() || value!.IsEmpty() || value!.IsWhitespace();
    }

    public static bool IsNullOrEmpty(this String value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNullOrWhiteSpace(this String value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNumeric(this String value)
    {
        return Double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double _);
    }

    public static bool IsPallindrome(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.ToLower().Equals(value.ToLower().ReverseString()))
            return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static bool IsPascalCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter()) && value.FirstOrDefault().IsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static bool IsTrue(this String value)
    {
        return value.Trim() == "1"
               || string.Compare(value.Trim(), "true", ignoreCase: true) == 0
               || string.Compare(value.Trim(), "t", ignoreCase: true) == 0
               || string.Compare(value.Trim(), "yes", ignoreCase: true) == 0
               || string.Compare(value.Trim(), "y", ignoreCase: true) == 0
               || string.Compare(value.Trim(), "ok", ignoreCase: true) == 0
               ;
    }

    public static bool IsValidEmailAddress(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return EmailAddress().Match(value).Success;
    }

    public static bool IsValidIPv4(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return IPv4().Match(value).Success;
    }

    public static bool IsValidNumber(this String value)
    {
        return IsValidNumber(value, System.Globalization.CultureInfo.CurrentCulture);
    }

    public static bool IsValidNumber(this String value, System.Globalization.CultureInfo culture)
    {
        string validNumberPattern = @"^-?(?:\d+|\d{1,3}(?:" + culture.NumberFormat.NumberGroupSeparator + @"\d{3})+)?(?:\" + culture.NumberFormat.NumberDecimalSeparator + @"\d+)?$";
        return new System.Text.RegularExpressions.Regex(validNumberPattern, System.Text.RegularExpressions.RegexOptions.ECMAScript).IsMatch(value);
    }

    public static bool IsWhitespace(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        int lastCharIndex = value.Length;
        int firstCharIndex = 0;
        while (true)
        {
            if (lastCharIndex > firstCharIndex)
                lastCharIndex--;

            if (!char.IsWhiteSpace(value[firstCharIndex]) || !char.IsWhiteSpace(value[lastCharIndex]))
                return false;

            if (firstCharIndex >= lastCharIndex)
                return true;

            firstCharIndex++;
        }
    }

    public static bool IsWordCharacter(string value, int pos)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        char c = value[pos];
        return char.IsLetterOrDigit(c)
               || c == '\''
               || (c == '.' && pos < value.Length - 1 && char.IsDigit(value[pos + 1]));
    }

    public static bool IsWordCharacter(System.Text.StringBuilder builder, int pos)
    {
        if (builder == default)
            throw new ArgumentNullException(nameof(builder));
        // ----------------------------------------------------------------------------------------------------
        char c = builder[pos];
        return char.IsLetterOrDigit(c)
               || c == '\''
               || (c == '.' && pos < builder.Length - 1 && char.IsDigit(builder[pos + 1]));
    }
}