namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class AssembleFromLinesExtensions
{
    #region Method(s): Private

    private static String AssembleFromLines(params String[] lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        // ----------------------------------------------------------------------------------------------------
        return String.Join("\n", lines);
    }

    #endregion

    public static String AssembleFromLines(this IEnumerable<String> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);
        // ----------------------------------------------------------------------------------------------------
        return AssembleFromLines(lines.ToArray());
    }
}