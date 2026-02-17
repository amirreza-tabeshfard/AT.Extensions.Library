namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class MatchExtensions
{
    public static Boolean Match(this String self, String pattern)
    {
        ArgumentException.ThrowIfNullOrEmpty(self);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.IsMatch(self, pattern);
    }
}