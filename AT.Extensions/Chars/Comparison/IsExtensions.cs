namespace AT.Extensions.Chars.Comparison;
public static class IsExtensions : Object
{
    public static Boolean IsDigit(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsDigit(ch);
    }

    public static Boolean IsLetter(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsLetter(ch);
    }

    public static Boolean IsLetterOrDigit(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsLetterOrDigit(ch);
    }

    public static Boolean IsLower(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsLower(ch);
    }

    public static Boolean IsNumeric(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        Boolean result = default;
        // ----------------------------------------------------------------------------------------------------
        if (ch.IsDigit())
            result = true;

        if (ch == '.' || ch == '-' || ch == ',')
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean IsPunctuation(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsPunctuation(ch);
    }

    public static Boolean IsSymbol(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsSymbol(ch);
    }

    public static Boolean IsUpper(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsUpper(ch);
    }

    public static Boolean? IsVowel(this Char ch)
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

    public static Boolean IsWhiteSpace(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.IsWhiteSpace(ch);
    }
}