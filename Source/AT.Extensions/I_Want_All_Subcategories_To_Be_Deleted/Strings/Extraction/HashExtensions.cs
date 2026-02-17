namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class HashExtensions
{
    public static Int32 Hash(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        UInt32 uiHash = default;
        // ----------------------------------------------------------------------------------------------------
        foreach (Byte letter in System.Text.Encoding.Unicode.GetBytes(value))
        {
            uiHash += letter;
            uiHash += (uiHash << 10);
            uiHash ^= (uiHash >> 6);
        }
        // ----------------------------------------------------------------------------------------------------
        uiHash += (uiHash << 3);
        uiHash ^= (uiHash >> 11);
        uiHash += (uiHash << 15);
        // ----------------------------------------------------------------------------------------------------
        return (Int32)(uiHash % Int32.MaxValue);
    }
}