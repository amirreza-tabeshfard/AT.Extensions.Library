namespace AT.Extensions.Strings.Extraction;
public static class DecryptExtensions : Object
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static String Decrypt(this String stringToDecrypt, String key)
    {
        ArgumentException.ThrowIfNullOrEmpty(stringToDecrypt);
        ArgumentException.ThrowIfNullOrEmpty(key);
        // ----------------------------------------------------------------------------------------------------
        System.Security.Cryptography.CspParameters cspParamters = new()
        {
            KeyContainerName = key
        };
        System.Security.Cryptography.RSACryptoServiceProvider rsaServiceProvider = new(cspParamters)
        {
            PersistKeyInCsp = true
        };
        String[] decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
        byte[] decryptByteArray = Array.ConvertAll(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));
        byte[] bytes = rsaServiceProvider.Decrypt(decryptByteArray, true);
        String result = System.Text.Encoding.UTF8.GetString(bytes);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}