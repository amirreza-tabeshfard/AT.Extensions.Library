namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
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