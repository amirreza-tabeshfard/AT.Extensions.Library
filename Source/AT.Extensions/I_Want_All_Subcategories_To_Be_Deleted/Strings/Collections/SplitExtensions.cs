namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class SplitExtensions
{
    public static IEnumerable<String> SplitAndTrim(this String value, params Char[] separators)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value
               .Trim()
               .Split(separators, StringSplitOptions.RemoveEmptyEntries)
               .Select(current => current.Trim());
    }

    public static IEnumerable<String> SplitCamelCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        const String PATTERN = @"[A-Z][a-z]*|[a-z]+|\d+";
        System.Text.RegularExpressions.MatchCollection matches = System.Text.RegularExpressions.Regex.Matches(value, PATTERN);
        ICollection<String> words = new List<String>();
        // ----------------------------------------------------------------------------------------------------
        foreach (System.Text.RegularExpressions.Match match in matches.Cast<System.Text.RegularExpressions.Match>())
            words.Add(match.Value);
        // ----------------------------------------------------------------------------------------------------
        return words;
    }

    public static String[] SplitIntoLines(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String cleaned = value.Replace("\r\n", "\n");
        String[] result = cleaned.Split(new[] { '\r', '\n' });
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}