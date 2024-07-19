using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;
using MimeDetective.Storage;

namespace AT.Extensions.Chars.Extraction;
public static class Extensions : Object
{
    #region Method(s): Private

    private static Char LastImpl(String source, Func<Char, Boolean> predicate, Boolean throwExceptionOnEmptyOrNotFound)
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
        Char[] chars = source.ToCharArray();
        Int32 count = source.Length;
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

    public static Char? FirstLetter(this String value)
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

    public static Char GetElementAt(this String value, Int32 index)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (index < 0 || index >= value.Length)
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Argument of of range. Requested character index {index} of a String with {value.Length} characters.");
        // ----------------------------------------------------------------------------------------------------
        return value[index];
    }

    public static Char GetElementAtOrDefault(this String value, Int32 index)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (index < 0 || index >= value.Length)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return value[index];
    }

    public static Char GetFirst(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (value.Length == default)
            throw new InvalidOperationException("The source String cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return value[0];
    }

    public static Char GetFirstOrDefault(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length == 0 ? default : value[0];
    }

    public static Char GetLast(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (value.Length == 0)
            throw new InvalidOperationException("Source String cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return value[value.Length - 1];
    }

    public static Char GetLast(this String value, Func<Char, Boolean> predicate)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else ArgumentNullException.ThrowIfNull(predicate);
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(value, predicate, true);
    }

    public static Char GetLastOrDefault(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length == 0 ? default : value[value.Length - 1];
    }

    public static Char GetLastOrDefault(this String value, Func<Char, Boolean> predicate)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else ArgumentNullException.ThrowIfNull(predicate);
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(value, predicate, false);
    }

    public static Char? LastLetter(this String value)
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