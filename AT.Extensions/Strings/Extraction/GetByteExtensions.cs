namespace AT.Extensions.Strings.Extraction;
public static class GetByteExtensions
{
    public static Int32 GetByteSize(this String value, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        return encoding.GetByteCount(value);
    }
}