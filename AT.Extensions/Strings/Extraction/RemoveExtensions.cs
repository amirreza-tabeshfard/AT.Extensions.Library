using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static partial class RemoveExtensions
{
    #region Field(s)

    private const String Quote = "\"";
    private const String SingleQuote = @"'";

    #endregion

    #region Method(s): Private

    [System.Text.RegularExpressions.GeneratedRegex("^\\s*$[\\r\\n]*", System.Text.RegularExpressions.RegexOptions.Multiline)]
    private static partial System.Text.RegularExpressions.Regex StripBlankLines();

    [System.Text.RegularExpressions.GeneratedRegex("\\s+")]
    private static partial System.Text.RegularExpressions.Regex NormalizeWhitespace();

    #endregion

    public static String RemoveAccents(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String normalizedString = value.Normalize(System.Text.NormalizationForm.FormD);
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(capacity: normalizedString.Length);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i < normalizedString.Length; i++)
        {
            Char c = normalizedString[i];
            System.Globalization.UnicodeCategory unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            // ----------------------------------------------------------------------------------------------------
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        // ----------------------------------------------------------------------------------------------------
        return stringBuilder
               .ToString()
               .Normalize(System.Text.NormalizationForm.FormC);

    }

    public static String RemoveAllWhitespace(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }

    public static String RemoveCharacters(this String value, List<Char> Chars)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(Chars);
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in Chars)
            result = result.Replace(c.ToString(), String.Empty);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RemoveCharactersInString(this String value, String removeUs)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(removeUs);
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in removeUs.ToCharArray())
            result = result.Replace(c.ToString(), "");
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RemoveChars(this String value, params Char[] Chars)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder stringBuilder = new(value.Length);
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value.Where(c => !Chars.Contains(c)))
            stringBuilder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return stringBuilder.ToString();
    }

    public static String RemoveDiacritical(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-7").GetBytes(value));
    }

    public static String RemoveDiacritics(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String normalizedString = value.Normalize(System.Text.NormalizationForm.FormD);
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in normalizedString)
        {
            System.Globalization.UnicodeCategory unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            // ----------------------------------------------------------------------------------------------------
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        // ----------------------------------------------------------------------------------------------------
        return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    public static String RemoveEmptyLines(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return StripBlankLines().Replace(value, String.Empty).Trim('\r', '\n');
    }

    public static String RemoveExcessWhiteSpace(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return NormalizeWhitespace().Replace(value, " ").Trim();
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }

    public static String RemoveLastOccuranceOf(this String value, String toMatch)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Contains(toMatch))
        {
            Int32 lastIndex = value.LastIndexOf(toMatch);
            return value.Substring(0, lastIndex);
        }
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String RemoveLastOccuranceOf(this String value, Char toMatch)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Contains(toMatch))
        {
            Int32 lastIndex = value.LastIndexOf(toMatch);
            return value.Substring(0, lastIndex);
        }
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String RemoveLines(this String value, Predicate<String> remove)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        if (remove != null)
        {
            List<String> lines = value.SplitIntoLines().ToList();
            for (Int32 i = lines.Count - 1; i >= 0; i--)
            {
                String line = lines[i];
                if (remove(line))
                    lines.RemoveAt(i);
            }
            result = lines.AssembleFromLines();
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RemoveNonAlphaNum(this String value, bool keepBlank = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(value, keepBlank ? "[^a-zA-Z\x20]" : "[^a-zA-Z]", ""), "\x20$", "");
    }

    public static String RemovePrefix(this String value, String prefix, bool ignoreCase = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.StartsWithIgnoreCase(prefix) : value.StartsWith(prefix))
            return value.Substring(prefix.Length, value.Length - prefix.Length);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String? RemoveSuffix(this String value, String suffix, bool ignoreCase = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.EndsWithIgnoreCase(suffix) : value.EndsWith(suffix))
            return value.Substring(0, value.Length - suffix.Length);
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static String RemoveWhiteSpace(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.ToCharArray()
                               .Where(c => !Char.IsWhiteSpace(c))
                               .ToArray());

    }

    public static String RemoveWrappingQuotes(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        foreach (String quote in new String[] { Quote, SingleQuote })
            if (value.StartsWithInvariant(quote) && value.EndsWithInvariant(quote) && value.Length > 1)
                return value.Substring(1, value.Length - 2);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }
}