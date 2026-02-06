namespace AT.Extensions.Strings.Collections;
public static class TextExtensions
{
    #region Method(s): Private

    private static Infrastructure.TextElementSegment CreateSegment(Int32 offset, Int32 length)
    {
        return new Infrastructure.TextElementSegment(offset, length);
    }

    #endregion

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

    public static IEnumerable<Infrastructure.TextElementSegment> TextElementSegments(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32[] elementOffsets = System.Globalization.StringInfo.ParseCombiningCharacters(value);
        Int32 lastOffset = -1;
        // ----------------------------------------------------------------------------------------------------
        foreach (Int32 offset in elementOffsets)
        {
            if (lastOffset != -1)
            {
                Int32 elementLength = offset - lastOffset;
                Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, elementLength);
                yield return segment;
            }

            lastOffset = offset;
        }
        // ----------------------------------------------------------------------------------------------------
        if (lastOffset != -1)
        {
            Int32 lastSegmentLength = value.Length - lastOffset;

            Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, lastSegmentLength);
            yield return segment;
        }
    }
}