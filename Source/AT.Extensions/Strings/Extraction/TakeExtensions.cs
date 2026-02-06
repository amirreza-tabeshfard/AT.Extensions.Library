namespace AT.Extensions.Strings.Extraction;
public static class TakeExtensions
{
    public static String? Take(this String value, Int32 count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return count <= 0
               ? default
               : value.Substring(0, Math.Min(count, value.Length));
    }
}