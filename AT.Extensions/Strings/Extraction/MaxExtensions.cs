namespace AT.Extensions.Strings.Extraction;
public static class MaxExtensions : Object
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