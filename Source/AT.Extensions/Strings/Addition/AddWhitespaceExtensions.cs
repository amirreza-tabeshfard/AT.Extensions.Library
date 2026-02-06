namespace AT.Extensions.Strings.Addition;
public static class AddWhitespaceExtensions
{
    public static String? AddWhitespaceLeft(this String value, Int32 length)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(new String(' ', length), value);
    }

    public static String? AddWhitespaceRight(this String value, Int32 length)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");
        // ----------------------------------------------------------------------------------------------------
        return String.Concat(value, new String(' ', length));
    }
}