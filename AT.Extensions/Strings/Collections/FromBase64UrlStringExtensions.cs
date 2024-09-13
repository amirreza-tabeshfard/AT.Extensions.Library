namespace AT.Extensions.Strings.Collections;
public static class FromBase64UrlStringExtensions : Object
{
    public static byte[]? FromBase64UrlStringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Contains("+") || value.Contains("/"))
        {
            if (shouldReturnNullIfConversionFailed)
                return default;

            throw new ArgumentException(AT.Infrastructure.ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));
        }
        // ----------------------------------------------------------------------------------------------------
        value = value.Replace("%3D", "=");
        int paddingRequired = value.Length % 4;
        string padding = paddingRequired == 0 ? String.Empty : new String('=', 4 - paddingRequired);
        string base64 = value.Replace("-", "+").Replace("_", "/") + padding;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException ex)
        {
            if (shouldReturnNullIfConversionFailed)
                return default;
            // ----------------------------------------------------------------------------------------------------
            if (ex.Message.Contains("illegal"))
                throw new ArgumentException(AT.Infrastructure.ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));
            // ----------------------------------------------------------------------------------------------------
            throw new ArgumentException(ex.Message, nameof(value), ex);
        }
    }
}