using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Addition;
public static class Extensions : Object
{
    public static String? AddWhitespaceLeft(this String value, Int32 length)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(new String(' ', length), value);
    }

    public static String? AddWhitespaceRight(this String value, Int32 length)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(value, new String(' ', length));
    }

    public static String Append(this String self, IEnumerable<String> lines, Boolean asAppendLine = false, Boolean appendWithWhiteSpace = false)
    {
        System.Text.StringBuilder builder = new();

        if (asAppendLine)
            builder.AppendLine(self);
        else
            builder.Append(self);

        String leadWith = String.Empty;
        if (appendWithWhiteSpace)
            leadWith = " ";

        String[] theLines = (String[])lines;
        String lastLine = theLines[theLines.Length - 1];

        foreach (String line in theLines)
        {
            if (asAppendLine && line == lastLine)
            {
                builder.Append(line);
                continue;
            }

            if (asAppendLine)
            {
                builder.AppendLine(line);
                continue;
            }

            builder.Append($"{leadWith}{line}");
        }
        return builder.ToString();
    }

    public static String AppendIf(this String str, Boolean condition, String appendMe)
    {
        if (condition)
        {
            return str + appendMe;
        }
        else
        {
            return str;
        }
    }

    public static String AppendPath(this String path, params String[] parts)
    {
        return Path.Combine(parts);
    }

    public static String AppendPrefixIfMissing(this String value, String prefix, Boolean ignoreCase = true)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.StartsWithIgnoreCase(prefix) : value.StartsWith(prefix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return prefix + value;
    }

    public static String AppendSuffixIfMissing(this String value, String suffix, Boolean ignoreCase = true)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.EndsWithIgnoreCase(suffix) : value.EndsWith(suffix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value + suffix;
    }

    public static String AppendSymbolIfMissing(this String? input, String symbol)
    {
        if (symbol.IsNullOrEmpty() || symbol.IsNullOrWhiteSpace())
            throw new ArgumentException(String.Format(AT.Infrastructure.ExceptionMessages.StringParamCannotBeNullOrEmpty_ParamName, nameof(symbol)), nameof(symbol));
        // ----------------------------------------------------------------------------------------------------
        input ??= String.Empty;
        return input.EndsWith(symbol)
            ? input
            : input + symbol;
    }

    public static String AppendUrl(this String url, String part)
    {
        var composite = (url ?? String.Empty).TrimEnd('/') + "/" + part.TrimStart('/');

        return composite.TrimEnd('/');
    }

    public static String CreateHashSha256(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder sb = new();
        using (System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create())
        {
            byte[] data = hash.ComputeHash(value.ToBytes());
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
        }
        // ----------------------------------------------------------------------------------------------------
        return sb.ToString();
    }

    public static String CreateHashSha512(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder sb = new();
        using (System.Security.Cryptography.SHA512 hash = System.Security.Cryptography.SHA512.Create())
        {
            byte[] data = hash.ComputeHash(value.ToBytes());
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
        }
        // ----------------------------------------------------------------------------------------------------
        return sb.ToString();
    }

    public static String CreateParameters(this String value, Boolean useOr)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        IDictionary<String, object> searchParamters = value.JsonToDictionary();
        System.Text.StringBuilder @params = new(String.Empty);
        
        if (searchParamters == default)
            return @params.ToString();

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

    public static String InsertCamelCaseSpaces(this String s)
    {
        System.Text.StringBuilder builder = new(s.Length * 2);

        Boolean lastIsUpper = false;
        Boolean lastIsWhitespace = false;

        for (Int32 i = 0; i < s.Length; i++)
        {
            Char c = s[i];
            Boolean isUpper = Char.IsUpper(c);
            Boolean nextIsLower = i + 1 < s.Length && Char.IsLower(s[i + 1]);

            if (isUpper && builder.Length > 0 && (!lastIsUpper || nextIsLower) && !lastIsWhitespace)
                builder.Append(' ');

            builder.Append(c);
            lastIsUpper = isUpper;
            lastIsWhitespace = Char.IsWhiteSpace(c);
        }

        return builder.ToString();
    }

    public static String InsertIntoEachLine(this String input, Int32 startIndex, String value)
    {
        String[] delims = { Environment.NewLine };
        var parts = input.Split(delims, StringSplitOptions.None);

        var results = new List<String>();
        foreach (var i in parts)
        {
            results.Add(i.PadRight(startIndex)
                         .Insert(startIndex, value));
        }
        return String.Join(Environment.NewLine, results);
    }
}