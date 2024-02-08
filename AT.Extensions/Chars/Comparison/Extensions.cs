namespace AT.Extensions.Chars.Comparison;
public static class Extensions : Object
{
    public static bool IsDigit(this char ch)
    {
        return char.IsDigit(ch);
    }

    public static bool IsLetter(this char ch)
    {
        return char.IsLetter(ch);
    }

    public static bool IsLetterOrDigit(this char ch)
    {
        return char.IsLetterOrDigit(ch);
    }

    public static bool IsLower(this char ch)
    {
        return char.IsLower(ch);
    }

    public static bool IsNumeric(this char ch)
    {
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
        return char.IsPunctuation(ch);
    }

    public static bool IsSymbol(this char ch)
    {
        return char.IsSymbol(ch);
    }

    public static bool IsUpper(this char ch)
    {
        return char.IsUpper(ch);
    }

    public static bool? IsVowel(this char c)
    {
        switch (c)
        {
            case 'a':
            case 'e':
            case 'i':
            case 'o':
            case 'u':
                return true;
            case 'y':
            case 'w':
                return null;
            default:
                return false;
        }
    }

    public static bool IsWhiteSpace(this char ch)
    {
        return char.IsWhiteSpace(ch);
    }
}