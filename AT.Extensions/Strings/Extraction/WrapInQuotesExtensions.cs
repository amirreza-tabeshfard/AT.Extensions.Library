namespace AT.Extensions.Strings.Extraction;
public static class WrapInQuotesExtensions : Object
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