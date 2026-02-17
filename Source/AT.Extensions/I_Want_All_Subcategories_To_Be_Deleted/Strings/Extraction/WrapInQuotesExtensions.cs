namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class WrapInQuotesExtensions
{
    #region Field(s)

    private const String Quote = "\"";

    #endregion

    public static String WrapInQuotes(this String value, String quote = Quote)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.StringWithPrefix(Quote).StringWithSuffix(Quote);
    }
}