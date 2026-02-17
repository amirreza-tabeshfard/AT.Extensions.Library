namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class FilterExtensions
{
    public static String Filter(this String value, Func<Char, Boolean> predicate)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value.Length);
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value)
            if (predicate(c))
                builder.Append(c);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }
}