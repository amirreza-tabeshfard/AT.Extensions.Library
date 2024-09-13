namespace AT.Extensions.Strings.Extraction;
public static class ShortenExtensions : Object
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