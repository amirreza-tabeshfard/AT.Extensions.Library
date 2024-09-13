using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class SortExtensions : Object
{
    public static String Sort(this String value, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value.Length);
        IComparer<Char> comparer = StartsWithExtensions.CharComparer.GetComparer(ignoreCase);
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value.OrderBy(c => c, comparer))
            builder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }
}