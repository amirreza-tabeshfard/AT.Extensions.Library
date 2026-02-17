using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Conversion;
using System.Reflection;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections.Generic;
public static class ToEnumExtensions
{
    public static TEnum ToEnum<TEnum>(this String text) 
        where TEnum : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        // ----------------------------------------------------------------------------------------------------
        Type enumType = typeof(TEnum);
        if (!enumType.GetTypeInfo().IsEnum) 
            throw new ArgumentException("{0} is not an Enum".ToFormat(enumType.Name));
        // ----------------------------------------------------------------------------------------------------
        return (TEnum)Enum.Parse(enumType, text, true);
    }

    public static T ToEnum<T>(this String value, T defaultValue = default) 
        where T : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!typeof(T).IsEnum)
            throw new ArgumentException("Type T Must of type System.Enum");
        // ----------------------------------------------------------------------------------------------------
        T result;
        Boolean isParsed = Enum.TryParse(value, true, out result);
        // ----------------------------------------------------------------------------------------------------
        return isParsed ? result : defaultValue;
    }

    public static TEnum? ToEnum<TEnum>(this String value, Boolean ignoreCase = false) 
        where TEnum : struct
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!typeof(TEnum).IsEnum)
            throw new ArgumentException($"Type {nameof(TEnum)} must be of type {typeof(System.Enum).FullName}");
        // ----------------------------------------------------------------------------------------------------
        Boolean isValid = Enum.TryParse(value, ignoreCase, out TEnum result);
        // ----------------------------------------------------------------------------------------------------
        if (!isValid)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}