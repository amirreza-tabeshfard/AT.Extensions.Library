using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Chars.Extraction;
public static class Extensions : Object
{
    #region Method(s): Private

    private static char LastImpl(string source, Func<char, bool> predicate, bool throwExceptionOnEmptyOrNotFound)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        else if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
        else if (source.Length == default)
        {
            if (throwExceptionOnEmptyOrNotFound)
                throw new InvalidOperationException("Source string cannot be empty but is.");
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
            throw new InvalidOperationException("Predicate test did not match any characters in source string: " + source);
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    #endregion

    public static char? FirstLetter(this String value)
    {
        var letters = value.KeepLettersOnly();
        if (string.IsNullOrEmpty(letters))
            return null;
        else
            return
                letters.GetFirst();
    }

    public static char GetElementAt(this String source, int index)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        else if (index < 0 || index >= source.Length)
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Argument of of range. Requested character index {index} of a string with {source.Length} characters.");
        // ----------------------------------------------------------------------------------------------------
        return source[index];
    }

    public static char GetElementAtOrDefault(this String source, int index)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        if (index < 0 || index >= source.Length)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return source[index];
    }

    public static char GetFirst(this String source)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        if (source.Length == default)
            throw new InvalidOperationException("The source string cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return source[0];
    }

    public static char GetFirstOrDefault(this String source)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.Length == 0 ? default : source[0];
    }

    public static char GetLast(this String source)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        else if (source.Length == 0)
            throw new InvalidOperationException("Source string cannot be empty but is.");
        // ----------------------------------------------------------------------------------------------------
        return source[source.Length - 1];
    }

    public static char GetLast(this String source, Func<char, bool> predicate)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        else if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(source, predicate, true);
    }

    public static char GetLastOrDefault(this String source)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.Length == 0 ? default : source[source.Length - 1];
    }

    public static char GetLastOrDefault(this String source, Func<char, bool> predicate)
    {
        if (string.IsNullOrEmpty(source))
            throw new ArgumentNullException(nameof(source));
        else if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
        // ----------------------------------------------------------------------------------------------------
        return LastImpl(source, predicate, false);
    }

    public static char? LastLetter(this String value)
    {
        var letters = value.KeepLettersOnly();
        if (string.IsNullOrEmpty(letters))
            return null;
        else
            return
                letters.GetLast();
    }
}