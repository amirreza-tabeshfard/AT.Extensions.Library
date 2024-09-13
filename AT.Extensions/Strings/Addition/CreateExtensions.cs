using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Addition;
public static class CreateExtensions : Object
{
    public static String CreateHashSha256(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder sb = new();
        Byte[] data = System.Security.Cryptography.SHA256.HashData(value.ToBytes());
        foreach (Byte b in data)
            sb.Append(b.ToString("x2"));
        // ----------------------------------------------------------------------------------------------------
        return sb.ToString();
    }

    public static String CreateHashSha512(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder sb = new();
        Byte[] data = System.Security.Cryptography.SHA512.HashData(value.ToBytes());
        foreach (Byte b in data)
            sb.Append(b.ToString("x2"));
        // ----------------------------------------------------------------------------------------------------
        return sb.ToString();
    }

    public static String CreateParameters(this String value, Boolean useOr)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IDictionary<String, object> searchParamters = value.JsonToDictionary();
        System.Text.StringBuilder @params = new(String.Empty);
        // ----------------------------------------------------------------------------------------------------
        if (searchParamters == default)
            return @params.ToString();
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i <= searchParamters.Count() - 1; i++)
        {
            String key = searchParamters.Keys.ElementAt(i);
            String val = (String)searchParamters[key];

            if (!key.IsNullOrEmpty() || key.IsNullOrWhiteSpace())
            {
                @params.Append(key).Append(" like '").Append(val.Trim()).Append("%' ");
                if (i < searchParamters.Count() - 1 && useOr)
                    @params.Append(" or ");
                else if (i < searchParamters.Count() - 1)
                    @params.Append(" and ");
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return @params.ToString();
    }
}