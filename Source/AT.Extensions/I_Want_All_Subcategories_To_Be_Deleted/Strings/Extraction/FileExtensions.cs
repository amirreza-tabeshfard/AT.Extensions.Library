using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Conversion;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class FileExtensions
{
    public static String FileEscape(this String file)
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        // ----------------------------------------------------------------------------------------------------
        return "\"{0}\"".ToFormat(file);
    }
}