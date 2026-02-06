namespace AT.Extensions.Strings.Extraction;
public static class RepeatExtensions
{
    public static String? Repeat(this String value, Int32 count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (count <= 0)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(Enumerable.Repeat(value, count));
    }
}