namespace AT.Extensions.Strings.Extraction;
public static class UnionEtensions : Object
{
    public static String Union(this String value, String unionChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Union(value, (IEnumerable<Char>)unionChars, ignoreCase);
    }

    public static String Union(this String value, IEnumerable<Char> unionChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 unionCharsCount = unionChars.Count();
        System.Text.StringBuilder builder = new(value.Length + unionCharsCount);
        HashSet<Char> hashSet = new(value.Length + unionCharsCount, Comparison.StartsWithExtensions.CharComparer.GetEqualityComparer(ignoreCase));
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value)
            if (hashSet.Add(c))
                builder.Append(c);

        foreach (Char c in unionChars)
            if (hashSet.Add(c))
                builder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }
}