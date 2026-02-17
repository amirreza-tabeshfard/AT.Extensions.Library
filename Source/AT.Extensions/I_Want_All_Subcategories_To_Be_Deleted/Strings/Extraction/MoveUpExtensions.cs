using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class MoveUpExtensions
{
    public static String? MoveUp(this String relativeUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(relativeUrl);
        // ----------------------------------------------------------------------------------------------------
        string[] segments = relativeUrl.Split('/');
        // ----------------------------------------------------------------------------------------------------
        if (segments.Length == 1)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return segments.Skip(1).Join("/");
    }
}