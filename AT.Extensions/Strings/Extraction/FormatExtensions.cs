namespace AT.Extensions.Strings.Extraction;
public static class FormatExtensions : Object
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