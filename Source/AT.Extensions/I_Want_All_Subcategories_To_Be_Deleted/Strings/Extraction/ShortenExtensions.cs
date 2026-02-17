namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class ShortenExtensions
{
    public static String Shorten(this String value, Int32 maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= maxLength)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(value.AsSpan(0, maxLength - 3), "[...]");
    }
}