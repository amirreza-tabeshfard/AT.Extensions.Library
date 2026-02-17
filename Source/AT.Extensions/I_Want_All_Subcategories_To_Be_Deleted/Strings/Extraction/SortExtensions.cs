using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class SortExtensions
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