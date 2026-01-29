namespace AT.Extensions.Strings.Extraction;
public static class SizeExtensions
{
    public static Int32 Size(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length * sizeof(Char);
    }

    public static Int32 Size(this String value, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        return encoding.GetByteCount(value);
    }

    public static Int32 SizeAs(this String value, System.Text.Encoding targetEncoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(targetEncoding);
        // ----------------------------------------------------------------------------------------------------
        return SizeAs(value, targetEncoding, System.Text.Encoding.Unicode);
    }

    public static Int32 SizeAs(this String value, System.Text.Encoding targetEncoding, System.Text.Encoding sourceEncoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(targetEncoding);
        ArgumentNullException.ThrowIfNull(sourceEncoding);
        // ----------------------------------------------------------------------------------------------------
        byte[] sourceBytes = sourceEncoding.GetBytes(value);
        byte[] targetBytes = System.Text.Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);
        // ----------------------------------------------------------------------------------------------------
        return targetBytes.Length;
    }
}