namespace AT.Extensions.Strings.Extraction;
public static class ExceptExtensions : Object
{
    public static String Except(this String value, IEnumerable<Char> exceptChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value.Length);
        HashSet<char> hashSet = new(exceptChars, Comparison.StartsWithExtensions.CharComparer.GetEqualityComparer(ignoreCase));
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value)
            if (!hashSet.Contains(c))
                builder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    public static String Except(this String value, String exceptChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Except(value, (IEnumerable<Char>)exceptChars, ignoreCase);
    }
}