namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class TextExtensions
{
    public static IEnumerable<String> TextElement(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(value);
        // ----------------------------------------------------------------------------------------------------
        while (elementEnumerator.MoveNext())
        {
            String textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }
}