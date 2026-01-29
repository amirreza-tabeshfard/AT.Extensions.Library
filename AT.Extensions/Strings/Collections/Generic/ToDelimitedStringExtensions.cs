namespace AT.Extensions.Strings.Collections.Generic;
public static class ToDelimitedStringExtensions
{
    public static String ToDelimitedString<T>(this IEnumerable<T>? collection, String delimiter = ", ", String? endDelimiter = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(delimiter);
        // ----------------------------------------------------------------------------------------------------
        if (collection is null)
            return String.Empty;
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder stringBuilder = new();
        using (IEnumerator<T> enumerator = collection.GetEnumerator())
        {
            if (enumerator.MoveNext())
                stringBuilder.Append(enumerator.Current);

            if (endDelimiter == default)
            {
                while (enumerator.MoveNext())
                    stringBuilder.Append(delimiter + enumerator.Current);
            }
            else
            {
                bool hasNextValue = enumerator.MoveNext();
                while (hasNextValue)
                {
                    T? current = enumerator.Current;
                    hasNextValue = enumerator.MoveNext();
                    if (!hasNextValue)
                    {
                        stringBuilder.Append(endDelimiter + current);
                        break;
                    }

                    stringBuilder.Append(delimiter + current);
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return stringBuilder.ToString();
    }

    public static String ToDelimitedString<T>(this IEnumerable<T>? collection, Func<T, String> func, String delimiter = ", ", String? endDelimiter = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(delimiter);
        // ----------------------------------------------------------------------------------------------------
        if (collection is null)
            return String.Empty;
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder stringBuilder = new();
        using (var enumerator = collection.GetEnumerator())
        {
            if (enumerator.MoveNext())
                stringBuilder.Append(func.Invoke(enumerator.Current));

            if (endDelimiter == default)
            {
                while (enumerator.MoveNext())
                    stringBuilder.Append(delimiter + func.Invoke(enumerator.Current));
            }
            else
            {
                bool hasNextValue = enumerator.MoveNext();
                while (hasNextValue)
                {
                    T? current = enumerator.Current;
                    hasNextValue = enumerator.MoveNext();
                    if (!hasNextValue)
                    {
                        stringBuilder.Append(endDelimiter + func.Invoke(current));
                        break;
                    }

                    stringBuilder.Append(delimiter + func.Invoke(current));
                }
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return stringBuilder.ToString();
    }
}