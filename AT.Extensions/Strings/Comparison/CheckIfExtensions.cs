namespace AT.Extensions.Strings.Comparison;
public static class CheckIfExtensions
{
    public static Boolean CheckIfPalindrome(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 counter = value.Length / 2;
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i < counter; i++)
            if (value[i].ToString().ToLower() != value[value.Length - 1 - i].ToString().ToLower())
                return false;
        // ----------------------------------------------------------------------------------------------------
        return true;
    }
}