namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class SkipExtensions
{
    public static String Skip(this String value, Int32 count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return count <= 0
               ? value
               : count >= value.Length
               ? String.Empty
               : value.Substring(count);
    }
}