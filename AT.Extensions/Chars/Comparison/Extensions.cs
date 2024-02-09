namespace AT.Extensions.Chars.Comparison;
public static class Extensions : Object
{
    public static bool IsDigit(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsDigit(ch);
    }

    public static bool IsLetter(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsLetter(ch);
    }

    public static bool IsLetterOrDigit(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsLetterOrDigit(ch);
    }

    public static bool IsLower(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsLower(ch);
    }

    public static bool IsNumeric(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        bool result = default;
        // ----------------------------------------------------------------------------------------------------
        if (ch.IsDigit())
            result = true;

        if (ch == '.' || ch == '-' || ch == ',')
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsPunctuation(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsPunctuation(ch);
    }

    public static bool IsSymbol(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsSymbol(ch);
    }

    public static bool IsUpper(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsUpper(ch);
    }

    public static bool? IsVowel(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return ch switch
        {
            'a' or 'e' or 'i' or 'o' or 'u' => true,
            'y' or 'w' => null,
            _ => false,
        };
    }

    public static bool IsWhiteSpace(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.IsWhiteSpace(ch);
    }
}