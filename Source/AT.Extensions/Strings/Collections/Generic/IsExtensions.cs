namespace AT.Extensions.Strings.Collections.Generic;
public static class IsExtensions
{
    public static Boolean Is<TValue>(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
        if (converter != null)
        {
            try
            {
                if (value != null)
                {
                    if (!converter.CanConvertFrom(default, value.GetType()))
                        return false;
                }
                converter.ConvertFrom(default, System.Globalization.CultureInfo.CurrentCulture, (Object)value);
                return true;
            }
            finally { }
        }
        return false;
    }
}