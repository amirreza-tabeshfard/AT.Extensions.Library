namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class DeleteExtensions
{
    public static String DeleteExtension(this String source, Char Delimeter = '.')
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        // ----------------------------------------------------------------------------------------------------
        if (source.IndexOf(Delimeter) < 0)
            return source;
        // ----------------------------------------------------------------------------------------------------
        return source.Substring(0, source.LastIndexOf(Delimeter));
    }
}