namespace AT.Extensions.Strings.Collections;
public static class QueryStringExtensions : Object
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