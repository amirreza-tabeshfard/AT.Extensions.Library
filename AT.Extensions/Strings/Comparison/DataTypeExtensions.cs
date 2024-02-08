namespace AT.Extensions.Strings.Comparison;
public static class DataTypeExtensions : Object
{
    public static bool IsByte(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Byte.TryParse(value, out _);
    }

    public static bool IsByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Byte.TryParse(value, style, provider, out _);
    }

    public static bool IsSByte(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return SByte.TryParse(value, out _);
    }

    public static bool IsSByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return SByte.TryParse(value, style, provider, out _);
    }

    public static bool IsShort(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int16.TryParse(value, out _);
    }

    public static bool IsShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int16.TryParse(value, style, provider, out _);
    }

    public static bool IsUShort(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt16.TryParse(value, out _);
    }

    public static bool IsUShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt16.TryParse(value, style, provider, out _);
    }

    public static bool IsInt(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int32.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
    }

    public static bool IsInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int32.TryParse(value, style, provider, out _);
    }

    public static bool IsUInt(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt32.TryParse(value, out _);
    }

    public static bool IsUInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt32.TryParse(value, style, provider, out _);
    }

    public static bool IsLong(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int64.TryParse(value, out _);
    }

    public static bool IsLong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Int64.TryParse(value, style, provider, out _);
    }

    public static bool IsULong(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt64.TryParse(value, out _);
    }

    public static bool IsULong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return UInt64.TryParse(value, style, provider, out _);
    }

    public static bool IsFloat(this String value)
    {
        return value.IsSingle();
    }

    public static bool IsFloat(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        return value.IsSingle(style, provider);
    }

    public static bool IsSingle(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Single.TryParse(value, out _);
    }

    public static bool IsSingle(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Single.TryParse(value, style, provider, out _);
    }

    public static bool IsDouble(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Double.TryParse(value, out _);
    }

    public static bool IsDouble(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Double.TryParse(value, style, provider, out _);
    }

    public static bool IsDecimal(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Decimal.TryParse(value, out _);
    }

    public static bool IsDecimal(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Decimal.TryParse(value, style, provider, out _);
    }

    public static bool IsChar(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return Char.TryParse(value, out _);
    }

    public static bool IsBool(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (!Boolean.TryParse(value, out bool result))
            switch (value.ToLower())
            {
                case "false": result = false; break;
                case "f": result = false; break;
                case "no": result = false; break;
                case "n": result = false; break;
                case "0": result = false; break;

                case "true": result = true; break;
                case "t": result = true; break;
                case "yes": result = true; break;
                case "y": result = true; break;
                case "1": result = true; break;
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsDateTime(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParse(value, out _);
    }
}