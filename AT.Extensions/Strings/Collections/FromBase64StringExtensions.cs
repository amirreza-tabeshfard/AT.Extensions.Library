namespace AT.Extensions.Strings.Collections;
public static class FromBase64StringExtensions : Object
{
    public static byte[]? FromBase64StringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return Convert.FromBase64String(value);
        }
        catch (FormatException ex)
        {
            if (shouldReturnNullIfConversionFailed)
                return default;

            throw new ArgumentException(ex.Message, nameof(value), ex);
        }
    }
}