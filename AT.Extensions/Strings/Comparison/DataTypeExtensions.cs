namespace AT.Extensions.Strings.Comparison;
public static class DataTypeExtensions : Object
{
    public static Boolean IsByte(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Byte.TryParse(value, out _);
    }

    public static Boolean IsByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Byte.TryParse(value, style, provider, out _);
    }

    public static Boolean IsSByte(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return SByte.TryParse(value, out _);
    }

    public static Boolean IsSByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return SByte.TryParse(value, style, provider, out _);
    }

    public static Boolean IsShort(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int16.TryParse(value, out _);
    }

    public static Boolean IsShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int16.TryParse(value, style, provider, out _);
    }

    public static Boolean IsUShort(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt16.TryParse(value, out _);
    }

    public static Boolean IsUShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt16.TryParse(value, style, provider, out _);
    }

    public static Boolean IsInt(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int32.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
    }

    public static Boolean IsInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int32.TryParse(value, style, provider, out _);
    }

    public static Boolean IsUInt(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt32.TryParse(value, out _);
    }

    public static Boolean IsUInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt32.TryParse(value, style, provider, out _);
    }

    public static Boolean IsLong(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int64.TryParse(value, out _);
    }

    public static Boolean IsLong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Int64.TryParse(value, style, provider, out _);
    }

    public static Boolean IsULong(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt64.TryParse(value, out _);
    }

    public static Boolean IsULong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return UInt64.TryParse(value, style, provider, out _);
    }

    public static Boolean IsFloat(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.IsSingle();
    }

    public static Boolean IsFloat(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.IsSingle(style, provider);
    }

    public static Boolean IsSingle(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Single.TryParse(value, out _);
    }

    public static Boolean IsSingle(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Single.TryParse(value, style, provider, out _);
    }

    public static Boolean IsDouble(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Double.TryParse(value, out _);
    }

    public static Boolean IsDouble(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Double.TryParse(value, style, provider, out _);
    }

    public static Boolean IsDecimal(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Decimal.TryParse(value, out _);
    }

    public static Boolean IsDecimal(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Decimal.TryParse(value, style, provider, out _);
    }

    public static Boolean IsChar(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Char.TryParse(value, out _);
    }

    public static Boolean IsBool(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Boolean.TryParse(value, out Boolean result))
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

    public static Boolean IsDateTime(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return DateTime.TryParse(value, out _);
    }
}