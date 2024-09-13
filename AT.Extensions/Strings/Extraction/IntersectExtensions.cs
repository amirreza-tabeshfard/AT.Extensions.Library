namespace AT.Extensions.Strings.Extraction;
public static class IntersectExtensions : Object
{
    public static String Intersect(this String value, IEnumerable<Char> intersectChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value.Length);
        HashSet<char> hashSet = new(value.Length, Comparison.StartsWithExtensions.CharComparer.GetEqualityComparer(ignoreCase));
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in intersectChars)
            hashSet.Add(c);

        foreach (Char c in value)
            if (hashSet.Remove(c))
                builder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    public static String Intersect(this String value, String intersectChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Intersect(value, (IEnumerable<Char>)intersectChars, ignoreCase);
    }
}