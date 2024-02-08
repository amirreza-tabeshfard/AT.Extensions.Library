using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Addition;
public static class Extensions : Object
{
    public static string? AddWhitespaceLeft(this String value, int length)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return string.Concat(new string(' ', length), value);
    }

    public static string? AddWhitespaceRight(this String value, int length)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return string.Concat(value, new string(' ', length));
    }

    public static string Append(this String self, IEnumerable<string> lines, bool asAppendLine = false, bool appendWithWhiteSpace = false)
    {
        System.Text.StringBuilder builder = new();

        if (asAppendLine)
            builder.AppendLine(self);
        else
            builder.Append(self);

        string leadWith = string.Empty;
        if (appendWithWhiteSpace)
            leadWith = " ";

        string[] theLines = (string[])lines;
        string lastLine = theLines[theLines.Length - 1];

        foreach (string line in theLines)
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

    public static string AppendIf(this String str, bool condition, string appendMe)
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

    public static string AppendPath(this String path, params string[] parts)
    {
        return Path.Combine(parts);
    }

    public static string AppendPrefixIfMissing(this String val, string prefix, bool ignoreCase = true)
    {
        if (string.IsNullOrEmpty(val) || (ignoreCase ? val.StartsWithIgnoreCase(prefix) : val.StartsWith(prefix)))
        {
            return val;
        }
        return prefix + val;
    }

    public static string AppendSuffixIfMissing(this String val, string suffix, bool ignoreCase = true)
    {
        if (string.IsNullOrEmpty(val) || (ignoreCase ? val.EndsWithIgnoreCase(suffix) : val.EndsWith(suffix)))
        {
            return val;
        }
        return val + suffix;
    }

    public static string AppendSymbolIfMissing(this string? input, string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException(string.Format(AT.Infrastructure.ExceptionMessages.StringParamCannotBeNullOrEmpty_ParamName, nameof(symbol)), nameof(symbol));

        input ??= string.Empty;
        return input.EndsWith(symbol)
            ? input
            : input + symbol;
    }

    public static string AppendUrl(this String url, string part)
    {
        var composite = (url ?? string.Empty).TrimEnd('/') + "/" + part.TrimStart('/');

        return composite.TrimEnd('/');
    }

    public static string CreateHashSha256(this string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentException("val");
        }
        var sb = new System.Text.StringBuilder();
        using (System.Security.Cryptography.SHA256 hash = System.Security.Cryptography.SHA256.Create())
        {
            byte[] data = hash.ComputeHash(val.ToBytes());
            foreach (byte b in data)
            {
                sb.Append(b.ToString("x2"));
            }
        }
        return sb.ToString();
    }

    public static string CreateHashSha512(this string val)
    {
        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentException("val");
        }
        var sb = new System.Text.StringBuilder();
        using (System.Security.Cryptography.SHA512 hash = System.Security.Cryptography.SHA512.Create())
        {
            byte[] data = hash.ComputeHash(val.ToBytes());
            foreach (byte b in data)
            {
                sb.Append(b.ToString("x2"));
            }
        }
        return sb.ToString();
    }

    public static string CreateParameters(this String value, bool useOr)
    {
        if (string.IsNullOrEmpty(value))
        {
            return string.Empty;
        }
        IDictionary<string, object> searchParamters = value.JsonToDictionary();
        var @params = new System.Text.StringBuilder("");
        if (searchParamters == null)
        {
            return @params.ToString();
        }
        for (int i = 0; i <= searchParamters.Count() - 1; i++)
        {
            string key = searchParamters.Keys.ElementAt(i);
            var val = (string)searchParamters[key];
            if (!string.IsNullOrEmpty(key))
            {
                @params.Append(key).Append(" like '").Append(val.Trim()).Append("%' ");
                if (i < searchParamters.Count() - 1 && useOr)
                {
                    @params.Append(" or ");
                }
                else if (i < searchParamters.Count() - 1)
                {
                    @params.Append(" and ");
                }
            }
        }
        return @params.ToString();
    }

    public static string InsertCamelCaseSpaces(this String s)
    {
        System.Text.StringBuilder builder = new(s.Length * 2);

        bool lastIsUpper = false;
        bool lastIsWhitespace = false;

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            bool isUpper = char.IsUpper(c);
            bool nextIsLower = i + 1 < s.Length && char.IsLower(s[i + 1]);

            if (isUpper && builder.Length > 0 && (!lastIsUpper || nextIsLower) && !lastIsWhitespace)
                builder.Append(' ');

            builder.Append(c);
            lastIsUpper = isUpper;
            lastIsWhitespace = char.IsWhiteSpace(c);
        }

        return builder.ToString();
    }

    public static string InsertIntoEachLine(this String input, int startIndex, string value)
    {
        string[] delims = { Environment.NewLine };
        var parts = input.Split(delims, StringSplitOptions.None);

        var results = new List<string>();
        foreach (var i in parts)
        {
            results.Add(i.PadRight(startIndex)
                         .Insert(startIndex, value));
        }
        return string.Join(Environment.NewLine, results);
    }
}