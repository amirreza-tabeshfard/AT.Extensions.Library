namespace AT.Extensions.Strings.Extraction;
public static class SafeExtensions
{
    public static Int32 SafeIndexOf(this String value, String substring, StringComparison comparison)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.IndexOf(substring, comparison);
    }

    public static Int32 SafeLength(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length;
    }
}