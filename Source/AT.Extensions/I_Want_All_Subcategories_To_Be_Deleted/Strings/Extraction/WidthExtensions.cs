namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class WidthExtensions
{
    public static Int32 Width(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(value);
        Int32 count = default;
        // ----------------------------------------------------------------------------------------------------
        while (elementEnumerator.MoveNext())
            count++;
        // ----------------------------------------------------------------------------------------------------
        return count;
    }
}