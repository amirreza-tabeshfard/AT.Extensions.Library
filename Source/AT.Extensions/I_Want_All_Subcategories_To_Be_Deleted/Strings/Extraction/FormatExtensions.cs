namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class FormatExtensions
{
    public static String FormatWith(this String value, object arg0)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Format(value, arg0);
    }

    public static String FormatWith(this String value, params object[] args)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Format(value, args);
    }
}