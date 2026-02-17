namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class QueryStringExtensions
{
    public static IDictionary<String, String>? QueryStringToDictionary(this String queryString)
    {
        ArgumentException.ThrowIfNullOrEmpty(queryString);
        // ----------------------------------------------------------------------------------------------------
        if (!queryString.Contains('?'))
            return default;
        // ----------------------------------------------------------------------------------------------------
        String query = queryString.Replace("?", "");
        if (!query.Contains('='))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return query
               .Split('&')
               .Select(p => p.Split('='))
               .ToDictionary(key => key[0].ToLower().Trim(), value => value[1]);
    }
}