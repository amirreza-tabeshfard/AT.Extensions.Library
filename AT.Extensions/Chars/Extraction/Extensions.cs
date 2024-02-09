using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;
using MimeDetective.Storage;

namespace AT.Extensions.Chars.Extraction;
public static class Extensions : Object
{
    #region Method(s): Private

    private static char LastImpl(String source, Func<char, bool> predicate, bool throwExceptionOnEmptyOrNotFound)
    {
        if (source.IsNullOrEmpty() || source.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(source));
        else if (predicate == default)
            throw new ArgumentNullException(nameof(predicate));
        else if (source.Length == default)
        {
            if (throwExceptionOnEmptyOrNotFound)
                throw new InvalidOperationException("Source String cannot be empty but is.");
            return default;
        }
        // ----------------------------------------------------------------------------------------------------
        char[] chars = source.ToCharArray();
        int count = source.Length;
        while (count-- > 0)
        {
            if (predicate(chars[count]))
                return chars[count];
        }
        // ----------------------------------------------------------------------------------------------------
        if (throwExceptionOnEmptyOrNotFound)
            throw new InvalidOperationException("Predicate test did not match any characters in source String: " + source);
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    #endregion

    public static char? FirstLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        String letters = value.KeepLettersOnly();
        if (!letters.IsNullOrEmpty() || !letters.IsNullOrWhiteSpace())
            return letters.GetFirst();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static char GetElementAt(this String value, int index)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (index < 0 || index >= value.Length)
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Argument of of range. Requested character index {index} of a String with {value.Length} characters.");
        // ----------------------------------------------------------------------------------------------------
        return value[index];
    }

    public static char GetElementAtOrDefault(this String value, int index)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (index < 0 || index >= value.Length)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return value[index];
    }

    public static char GetFirst(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (value.Length == default)
            throw new InvalidOperationException("The source String cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return value[0];
    }

    public static char GetFirstOrDefault(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length == 0 ? default : value[0];
    }

    public static char GetLast(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (value.Length == 0)
            throw new InvalidOperationException("Source String cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return value[value.Length - 1];
    }

    public static char GetLast(this String value, Func<char, bool> predicate)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else ArgumentNullException.ThrowIfNull(predicate);
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(value, predicate, true);
    }

    public static char GetLastOrDefault(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length == 0 ? default : value[value.Length - 1];
    }

    public static char GetLastOrDefault(this String value, Func<char, bool> predicate)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else ArgumentNullException.ThrowIfNull(predicate);
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(value, predicate, false);
    }

    public static char? LastLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        String letters = value.KeepLettersOnly();
        if (!letters.IsNullOrEmpty() || !letters.IsNullOrWhiteSpace())
            return letters.GetLast();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}