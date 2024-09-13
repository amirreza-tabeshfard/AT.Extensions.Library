namespace AT.Extensions.Strings.Comparison;
public static class MatchExtensions : Object
{
    public static Boolean Match(this String self, String pattern)
    {
        ArgumentException.ThrowIfNullOrEmpty(self);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.IsMatch(self, pattern);
    }
}