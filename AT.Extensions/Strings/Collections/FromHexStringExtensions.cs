namespace AT.Extensions.Strings.Collections;
public static class FromHexStringExtensions
{
    #region Field(s)

    private static readonly Dictionary<Char, byte> CharToNibbleMapping = new Dictionary<Char, byte>
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'A', 10 }, { 'a', 10}, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 }, { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }
        };

    #endregion

    public static byte[]? FromHexStringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        value = value.StartsWith("0x")
            ? value[2..]
            : value;
        // ----------------------------------------------------------------------------------------------------
        Byte[] output = new Byte[value.Length / 2];
        if (value.Length % 2 != 0)
        {
            if (shouldReturnNullIfConversionFailed) return null;
            throw new ArgumentException(Infrastructure.ExceptionMessages.HexStringInvalidLength, nameof(value));
        }
        // ----------------------------------------------------------------------------------------------------
        Int32 outputIndex = 0;
        for (Int32 idx = 0; idx < value.Length; idx += 2, outputIndex += 1)
        {
            try
            {
                Byte highNibble = CharToNibbleMapping[value[idx]];
                Byte lowNibble = CharToNibbleMapping[value[idx + 1]];
                output[outputIndex] = (Byte)((highNibble << 4) | lowNibble);
            }
            catch (KeyNotFoundException)
            {
                if (shouldReturnNullIfConversionFailed) 
                    return default;

                throw new ArgumentException(Infrastructure.ExceptionMessages.HexStringHasIllegalCharacter, nameof(value));
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return output;
    }
}