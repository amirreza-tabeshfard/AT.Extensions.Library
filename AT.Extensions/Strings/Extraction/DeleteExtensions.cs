namespace AT.Extensions.Strings.Extraction;
public static class DeleteExtensions : Object
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