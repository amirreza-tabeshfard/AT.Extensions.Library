namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class MidExtensions
{
    public static String Mid(this String value, Int32 startIndex)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Mid(value, startIndex, Int32.MaxValue);
    }

    public static String Mid(this String value, Int32 startIndex, Int32 length)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= startIndex)
            return String.Empty;
        else if (length == Int32.MaxValue || value.Length <= startIndex + length)
            return value.Substring(startIndex);
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(startIndex, length);
    }
}