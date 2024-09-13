namespace AT.Extensions.Strings.Extraction;
public static class DataTypeExtensions : Object
{
    public static Byte AsByte(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Byte.TryParse(value, out Byte result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Byte AsByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Byte.TryParse(value, style, provider, out Byte result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static SByte AsSByte(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!SByte.TryParse(value, out SByte result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static SByte AsSByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!SByte.TryParse(value, style, provider, out SByte result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int16 AsShort(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int16.TryParse(value, out Int16 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int16 AsShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int16.TryParse(value, style, provider, out Int16 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt16 AsUShort(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt16.TryParse(value, out UInt16 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt16 AsUShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt16.TryParse(value, style, provider, out UInt16 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int32 AsInt(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int32.TryParse(value, out Int32 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int32 AsInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int32.TryParse(value, style, provider, out Int32 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt32 AsUInt(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt32.TryParse(value, out UInt32 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt32 AsUInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt32.TryParse(value, style, provider, out UInt32 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int64 AsLong(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int64.TryParse(value, out Int64 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int64 AsLong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Int64.TryParse(value, style, provider, out Int64 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt64 AsULong(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt64.TryParse(value, out UInt64 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static UInt64 AsULong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!UInt64.TryParse(value, style, provider, out UInt64 result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Single AsFloat(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Single.TryParse(value, out Single result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Single AsFloat(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Single.TryParse(value, style, provider, out Single result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Single AsSingle(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Single.TryParse(value, out Single result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Single AsSingle(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Single.TryParse(value, style, provider, out Single result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Double AsDouble(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Double.TryParse(value, out Double result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Double AsDouble(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Double.TryParse(value, style, provider, out Double result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Decimal AsDecimal(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Decimal.TryParse(value, out Decimal result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Decimal AsDecimal(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Decimal.TryParse(value, style, provider, out Decimal result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Char AsChar(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!Char.TryParse(value, out Char result))
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}