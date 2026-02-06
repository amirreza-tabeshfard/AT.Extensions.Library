namespace AT.Extensions.Strings.Extraction;
public static class EncryptExtensions
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static String Encrypt(this String stringToEncrypt, String key)
    {
        ArgumentException.ThrowIfNullOrEmpty(stringToEncrypt);
        ArgumentException.ThrowIfNullOrEmpty(key);
        // ----------------------------------------------------------------------------------------------------
        System.Security.Cryptography.CspParameters cspParameter = new()
        {
            KeyContainerName = key
        };
        System.Security.Cryptography.RSACryptoServiceProvider rsaServiceProvider = new(cspParameter)
        {
            PersistKeyInCsp = true
        };
        Byte[] bytes = rsaServiceProvider.Encrypt(System.Text.Encoding.UTF8.GetBytes(stringToEncrypt), true);
        // ----------------------------------------------------------------------------------------------------
        return BitConverter.ToString(bytes);
    }
}