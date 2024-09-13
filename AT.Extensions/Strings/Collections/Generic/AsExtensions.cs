namespace AT.Extensions.Strings.Collections.Generic;
public static class AsExtensions : Object
{
    public static TValue As<TValue>(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.As<TValue>(default);
    }

    public static TValue As<TValue>(this String value, TValue defaultValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.ComponentModel.TypeConverter converter1 = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
            if (converter1.CanConvertFrom(typeof(String)))
                return (TValue)converter1.ConvertFrom((Object)value);
            // ----------------------------------------------------------------------------------------------------
            System.ComponentModel.TypeConverter converter2 = System.ComponentModel.TypeDescriptor.GetConverter(typeof(String));
            if (converter2.CanConvertTo(typeof(TValue)))
                return (TValue)converter2.ConvertTo((Object)value, typeof(TValue));
        }
        finally { }
        // ----------------------------------------------------------------------------------------------------
        return defaultValue;
    }
}