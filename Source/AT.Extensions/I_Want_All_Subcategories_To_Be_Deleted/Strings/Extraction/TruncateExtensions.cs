using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class TruncateExtensions
{
    public static String Truncate(this String value, Int32 maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (maxLength <= 0)
            return String.Empty;

        if (value.Length > maxLength)
            return value.Substring(0, maxLength) + "...";
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String Truncate(this String value, Int32 length, String ellipsis)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Truncate(value, length, ellipsis, true);
    }

    public static String Truncate(this String value, Int32 length, String ellipsis, Boolean inclusiveEllipsis)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Truncate(value, length, ellipsis, inclusiveEllipsis, default, false, StringComparison.Ordinal);
    }

    public static String Truncate(this String value, Int32 length, String ellipsis, Boolean inclusiveEllipsis, String boundary, Boolean emptyOnNoBoundary, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(ellipsis);
        // ----------------------------------------------------------------------------------------------------
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "length cant be smaller than 0");

        if (inclusiveEllipsis)
            if (ellipsis.Length > length)
                throw new ArgumentException("Ellipsis cant be larger than the desired length when inclusiveEllipsis is set", "ellipsis");
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        // ----------------------------------------------------------------------------------------------------
        if (value.Length > length)
        {
            Int32 checkLength = length;

            if (inclusiveEllipsis && !ellipsis.IsNullOrEmpty())
                checkLength -= ellipsis.Length;

            if (!boundary.IsNullOrEmpty() || !boundary.IsNullOrWhiteSpace())
            {
                Int32 boundaryIndex = value.LastIndexOf(boundary, checkLength, checkLength, comparisonType);
                if (boundaryIndex != -1)
                {
                    Int32 boundaryLength = boundaryIndex; // we want to stop right before the boundary starts so we can use the index of the boundary as the length.

                    result = value.Left(boundaryLength);
                }
                else
                {
                    if (emptyOnNoBoundary)
                        result = String.Empty;
                    else
                        result = value.Left(length);
                }
            }
            else
                result = value.Left(checkLength);
            // ----------------------------------------------------------------------------------------------------
            if (!ellipsis.IsNullOrEmpty() || !ellipsis.IsNullOrWhiteSpace())
                result += ellipsis;
        }
        else
        {
            if (!inclusiveEllipsis)
                if (ellipsis != null)
                    result += ellipsis;
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String Truncate(this String value, Int32 maxLength, Boolean smartTrim = true, Boolean appendEllipsis = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        const String ellipsis = "...";
        // ----------------------------------------------------------------------------------------------------
        if (maxLength < 0)
            throw new ArgumentException($"{nameof(maxLength)} cannot be less than zero.");
        // ----------------------------------------------------------------------------------------------------
        if (value.Length > maxLength)
        {
            if (appendEllipsis)
            {
                if (maxLength > ellipsis.Length)
                    maxLength -= ellipsis.Length;
                else
                    appendEllipsis = false;
            }
            // ----------------------------------------------------------------------------------------------------
            Int32 length = maxLength;
            // ----------------------------------------------------------------------------------------------------
            if (smartTrim)
            {
                while (length > 0 && Char.IsWhiteSpace(value[length - 1]))
                    length--;

                if (length == 0)
                    length = maxLength;
            }
            // ----------------------------------------------------------------------------------------------------
            value = value.Substring(0, length);
            // ----------------------------------------------------------------------------------------------------
            if (appendEllipsis)
                value += ellipsis;
        }
        // ----------------------------------------------------------------------------------------------------
        return value;
    }
}