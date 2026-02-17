using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
using System.Reflection;

namespace AT.Extensions.Strings.Extraction;
public static partial class ReplaceExtensions
{
    #region Field(s)

    private static readonly System.Text.RegularExpressions.RegexOptions InvariantCultureIgnoreCaseRegexOptions = System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant;

    #endregion

    #region Method(s): Private

    private static Dictionary<String, Object?> ExtractKeyValues(Object keyValues)
    {
        PropertyInfo[] props = keyValues.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return props.ToDictionary(prop => prop.Name, prop => prop.GetValue(keyValues));
    }

    [System.Text.RegularExpressions.GeneratedRegex("^[\\r\\n]+|\\.|[\\r\\n]+$")]
    private static partial System.Text.RegularExpressions.Regex StripNewlinesAndDots();

    #endregion

    public static String Replace(this String value, params Char[] chars)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return chars.Aggregate(value, (current, c) => current.Replace(c.ToString(System.Globalization.CultureInfo.InvariantCulture), ""));
    }

    public static String Replace(this String value, String oldValue, String newValue, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(oldValue);
        ArgumentException.ThrowIfNullOrEmpty(newValue);
        // ----------------------------------------------------------------------------------------------------
        return value.Replace(oldValue, newValue, 0, value.Length, comparisonType);
    }

    public static String Replace(this String value, String oldValue, String newValue, int startIndex, int count, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(oldValue);
        ArgumentException.ThrowIfNullOrEmpty(newValue);
        // ----------------------------------------------------------------------------------------------------
        return value.Replace(oldValue, newValue, startIndex, count, comparisonType, out int total);
    }

    public static String Replace(this String value, String oldValue, String newValue, int startIndex, int count, StringComparison comparisonType, out int total)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(oldValue);
        ArgumentException.ThrowIfNullOrEmpty(newValue);
        // ----------------------------------------------------------------------------------------------------
        if (startIndex < 0 || startIndex >= value.Length)
            throw new ArgumentOutOfRangeException(nameof(startIndex), "startIndex should be between 0 and input.Length");

        if (count < 0 || count > value.Length - startIndex)
            throw new ArgumentOutOfRangeException(nameof(count), "count should be larger or equal to 0 and smaller than input.Length - startIndex");
        // ----------------------------------------------------------------------------------------------------
        String result;
        int initialLength = Math.Max(value.Length - oldValue.Length, 0);
        // ----------------------------------------------------------------------------------------------------
        if (newValue != null)
            initialLength += newValue.Length;
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder(initialLength);
        // ----------------------------------------------------------------------------------------------------
        if (startIndex > 0)
            resultBuilder.Append(value, 0, startIndex);
        // ----------------------------------------------------------------------------------------------------
        int currentIndex = startIndex;
        int maxIndex = startIndex + count;
        total = default;
        // ----------------------------------------------------------------------------------------------------
        while (currentIndex < maxIndex)
        {
            int lastIndex = currentIndex;
            int newIndex = value.IndexOf(oldValue, lastIndex, maxIndex - lastIndex, comparisonType);
            // ----------------------------------------------------------------------------------------------------
            if (newIndex != -1)
            {
                resultBuilder.Append(value, lastIndex, newIndex - lastIndex);
                resultBuilder.Append(newValue);

                currentIndex = newIndex + oldValue.Length;

                total++;
            }
            else
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        int finalCount = value.Length - currentIndex;
        resultBuilder.Append(value, currentIndex, finalCount);
        // ----------------------------------------------------------------------------------------------------
        result = resultBuilder.ToString();
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String ReplaceIgnoreCase(this String value, String oldValue, String newValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? String.Empty),
            newValue ?? String.Empty,
            InvariantCultureIgnoreCaseRegexOptions
        );
    }

    public static String ReplaceIgnoreCase(this String value, String oldValue, Func<String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? String.Empty),
            replaceCallback
        );
    }

    public static String ReplaceIgnoreCase(this String value, String oldValue, Func<int, String, String> replaceCallback)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? String.Empty),
            replaceCallback
        );
    }

    public static String ReplaceLineFeeds(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return StripNewlinesAndDots().Replace(value, "");
    }

    public static String ReplaceWhiteSpace(this String value, String replacement = "", bool groupreplace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace(value, groupreplace ? @"[\s]+" : @"\s", replacement);
    }
}