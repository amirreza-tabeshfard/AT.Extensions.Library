using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class IndentExtensions
{
    public static String? IndentEachLine(this String value, String prefix = "  ")
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String? result;
        // ----------------------------------------------------------------------------------------------------
        String[] split = value.SplitIntoLines();
        result = string.Empty;
        Boolean first = true;
        // ----------------------------------------------------------------------------------------------------
        foreach (String line in split)
        {
            if (first)
                first = false;
            else
                result += Environment.NewLine;

            result += (prefix + split);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}