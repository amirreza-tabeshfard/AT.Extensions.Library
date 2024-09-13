using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class FileExtensions : Object
{
    public static String FileEscape(this String file)
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        // ----------------------------------------------------------------------------------------------------
        return "\"{0}\"".ToFormat(file);
    }
}