namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class AsBase64Extensions
{
    public static String AsBase64Decoded(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (String.IsNullOrWhiteSpace(value))
            return value;
        // ----------------------------------------------------------------------------------------------------
        byte[] base64EncodedBytes = Convert.FromBase64String(value);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static String AsBase64Encoded(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (String.IsNullOrWhiteSpace(value))
            return value;
        // ----------------------------------------------------------------------------------------------------
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
        // ----------------------------------------------------------------------------------------------------
        return Convert.ToBase64String(plainTextBytes);
    }
}