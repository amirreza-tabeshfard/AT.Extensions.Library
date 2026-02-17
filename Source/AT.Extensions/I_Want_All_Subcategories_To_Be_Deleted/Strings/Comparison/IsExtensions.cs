using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Comparison;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;

namespace AT.Extensions.Strings.Comparison;
public static partial class IsExtensions
{
    #region Method(s): Private

    private static Boolean PassesSanityCheckForCases(this String value)
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

    public static Boolean IsAllDigits(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value)
            if (c < '0' || c > '9')
                return default;
        // ----------------------------------------------------------------------------------------------------
        return true;
    }

    public static Boolean IsCamelCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter()) && value.FirstOrDefault().IsLower();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean IsDateTime(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParse(value, out DateTime _);
    }

    public static Boolean IsDateTime(this String value, String format)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (format.IsNullOrEmpty() || format.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(format));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime _);
    }

    public static Boolean IsEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(false)] this String value)
    {
        return (String.IsNullOrWhiteSpace(value)) && (value.Length == 0);
    }

    public static Boolean IsEmptyOrWhiteSpace(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        Boolean result;
        if (value.IsEmpty())
            result = true;
        else
        {
            result = true;
            foreach (Char character in value)
                if (!Char.IsWhiteSpace(character))
                {
                    result = false;
                    break;
                }
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean IsEndOfSentenceCharacter(String value, Int32 pos)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        Char c = value[pos];
        return c == '!' ||
               c == '?' ||
               c == ':' ||
               (c == '.' && !(pos < (value.Length - 1) && Char.IsDigit(value[pos + 1])));
    }

    public static Boolean IsEndOfSentenceCharacter(System.Text.StringBuilder builder, Int32 pos)
    {
        if (builder == default)
            throw new ArgumentNullException(nameof(builder));
        // ----------------------------------------------------------------------------------------------------
        Char c = builder[pos];
        return c == '!' ||
               c == '?' ||
               c == ':' ||
               (c == '.' && !(pos < (builder.Length - 1) && Char.IsDigit(builder[pos + 1])));
    }

    public static Boolean IsHungarianNotation(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter() || x == '_') && !value.ContainsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean IsKebabCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter() || x == '-') && !value.ContainsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean IsLengthBetween(this String value, Int32 minimum, Int32 maximum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsMinLength(minimum)
               && value.IsMaxLength(maximum);
    }

    public static Boolean IsLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Trim().Replace(oldValue: " ", newValue: String.Empty).All(Char.IsLetter);
    }

    public static Boolean IsLetterOrDigit(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Trim().Replace(oldValue: " ", newValue: String.Empty).All(Char.IsLetterOrDigit);
    }

    public static Boolean IsMaxLength(this String value, Int32 maximum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length <= maximum;
    }

    public static Boolean IsMinLength(this String value, Int32 minimum)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length >= minimum;
    }

    public static Boolean IsNotNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this String? value)
    {
        return !value.IsNullOrEmpty();
    }

    public static void IsNotNullOrEmpty([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this String? value, Action<String> action)
    {
        if (value.IsNotNullOrEmpty())
            action(value);
    }

    public static Boolean IsNotNullOrWhiteSpace(this String value)
    {
        return !String.IsNullOrWhiteSpace(value);
    }

    public static Boolean IsNull(this String value)
    {
        return (value is null) || (value == default);
    }

    public static Boolean IsNullEmptyOrWhitespace([System.Diagnostics.CodeAnalysis.NotNullWhen(true)] this String value)
    {
        return value.IsNull() || value!.IsEmpty() || value!.IsWhitespace();
    }

    public static Boolean IsNullOrEmpty(this String value)
    {
        return String.IsNullOrEmpty(value);
    }

    public static Boolean IsNullOrWhiteSpace(this String value)
    {
        return String.IsNullOrWhiteSpace(value);
    }

    public static Boolean IsNumeric(this String value)
    {
        return Double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out Double _);
    }

    public static Boolean IsPallindrome(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.ToLower().Equals(value.ToLower().ReverseString()))
            return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean IsPascalCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.PassesSanityCheckForCases())
            return value.All(x => x.IsLetter()) && value.FirstOrDefault().IsUpper();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean IsTrue(this String value)
    {
        return value.Trim() == "1"
               || String.Compare(value.Trim(), "true", ignoreCase: true) == 0
               || String.Compare(value.Trim(), "t", ignoreCase: true) == 0
               || String.Compare(value.Trim(), "yes", ignoreCase: true) == 0
               || String.Compare(value.Trim(), "y", ignoreCase: true) == 0
               || String.Compare(value.Trim(), "ok", ignoreCase: true) == 0
               ;
    }

    public static Boolean IsValidEmailAddress(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return EmailAddress().Match(value).Success;
    }

    public static Boolean IsValidIPv4(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return IPv4().Match(value).Success;
    }

    public static Boolean IsValidNumber(this String value)
    {
        return IsValidNumber(value, System.Globalization.CultureInfo.CurrentCulture);
    }

    public static Boolean IsValidNumber(this String value, System.Globalization.CultureInfo culture)
    {
        String validNumberPattern = @"^-?(?:\d+|\d{1,3}(?:" + culture.NumberFormat.NumberGroupSeparator + @"\d{3})+)?(?:\" + culture.NumberFormat.NumberDecimalSeparator + @"\d+)?$";
        return new System.Text.RegularExpressions.Regex(validNumberPattern, System.Text.RegularExpressions.RegexOptions.ECMAScript).IsMatch(value);
    }

    public static Boolean IsWhitespace(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        Int32 lastCharIndex = value.Length;
        Int32 firstCharIndex = 0;
        while (true)
        {
            if (lastCharIndex > firstCharIndex)
                lastCharIndex--;

            if (!Char.IsWhiteSpace(value[firstCharIndex]) || !Char.IsWhiteSpace(value[lastCharIndex]))
                return false;

            if (firstCharIndex >= lastCharIndex)
                return true;

            firstCharIndex++;
        }
    }

    public static Boolean IsWordCharacter(String value, Int32 pos)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        Char c = value[pos];
        return Char.IsLetterOrDigit(c)
               || c == '\''
               || (c == '.' && pos < value.Length - 1 && Char.IsDigit(value[pos + 1]));
    }

    public static Boolean IsWordCharacter(System.Text.StringBuilder builder, Int32 pos)
    {
        if (builder == default)
            throw new ArgumentNullException(nameof(builder));
        // ----------------------------------------------------------------------------------------------------
        Char c = builder[pos];
        return Char.IsLetterOrDigit(c)
               || c == '\''
               || (c == '.' && pos < builder.Length - 1 && Char.IsDigit(builder[pos + 1]));
    }
}