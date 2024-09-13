namespace AT.Extensions.Strings.Extraction;
public static class WidthExtensions : Object
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