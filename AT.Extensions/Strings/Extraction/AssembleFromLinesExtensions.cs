namespace AT.Extensions.Strings.Extraction;
public static class AssembleFromLinesExtensions : Object
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