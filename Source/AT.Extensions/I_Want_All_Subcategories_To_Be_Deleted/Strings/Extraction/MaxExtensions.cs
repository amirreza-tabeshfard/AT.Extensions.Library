namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class MaxExtensions
{
    public static String MaxLengthTrim(this String value, Int32 maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length > maxLength)
            return value.Substring(0, maxLength - 1);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }
}