namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class ExcerptExtensions
{
    public static String Excerpt(this String value, Int32 characters)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= characters)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return string.Concat(value.AsSpan(0, characters), "[...]");
    }
}