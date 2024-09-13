namespace AT.Extensions.Strings.Extraction;
public static class SkipExtensions : Object
{
    public static String Skip(this String value, Int32 count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return count <= 0
               ? value
               : count >= value.Length
               ? String.Empty
               : value.Substring(count);
    }
}