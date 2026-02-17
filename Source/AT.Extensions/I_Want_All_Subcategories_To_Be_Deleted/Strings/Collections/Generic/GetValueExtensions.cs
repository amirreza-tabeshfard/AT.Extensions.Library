using System.Reflection;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections.Generic;
public static class GetValueExtensions
{
    public static T GetValue<T>(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        MethodInfo mi = typeof(T).GetMethod("Parse", new Type[] { typeof(String) });
        if (mi != null)
            return (T)mi.Invoke(typeof(T), new object[] { value });
        else if (typeof(T).IsEnum)
        {
            if (Enum.TryParse(typeof(T), value, out object ev))
                return (T)(object)ev;
            else
                throw new ArgumentException($"{value} is not a valid member of {typeof(T).Name}");
        }
        else
            throw new ArgumentException($"No conversion supported for {typeof(T).Name}");
    }

    public static T GetValue<T>(this String value, T defaultValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        MethodInfo mi = typeof(T).GetMethod("TryParse", new Type[] { typeof(String), typeof(T).MakeByRefType() });
        if (mi != null)
        {
            T v;
            var p = new object[] { value, null };
            if ((Boolean)mi.Invoke(null, p))
                return (T)p[1];
            else
                return defaultValue;
        }
        else if (typeof(T).IsEnum && System.Enum.TryParse(typeof(T), value, out object ev))
            return (T)ev;
        else
            return defaultValue;
    }
}