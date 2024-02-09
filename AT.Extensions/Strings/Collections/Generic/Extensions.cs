using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Conversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

namespace AT.Extensions.Strings.Collections.Generic;
public static class Extensions : Object
{
    public static TValue As<TValue>(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.As<TValue>(default(TValue));
    }

    public static TValue As<TValue>(this String value, TValue defaultValue)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.ComponentModel.TypeConverter converter1 = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
            if (converter1.CanConvertFrom(typeof(String)))
                return (TValue)converter1.ConvertFrom((object)value);
            System.ComponentModel.TypeConverter converter2 = System.ComponentModel.TypeDescriptor.GetConverter(typeof(String));
            if (converter2.CanConvertTo(typeof(TValue)))
                return (TValue)converter2.ConvertTo((object)value, typeof(TValue));
        }
        finally
        {
        }
        // ----------------------------------------------------------------------------------------------------
        return defaultValue;
    }

    public static T Deserialize<T>(this String jsonString)
    {
        if (jsonString.IsEmpty()) return default(T);
        return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
    }

    public static IEnumerable<TFuncResult> ForEach<TFuncResult>(this String self, Func<char, TFuncResult> function)
    {
        IList<TFuncResult> items = new List<TFuncResult>();
        foreach (char character in self)
        {
            TFuncResult? result = function(character);

            if (result is null)
                continue;

            items.Add(result);
        }
        return items;
    }

    public static T FromJson<T>(this String json, params Newtonsoft.Json.JsonConverter[] converters)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, converters);
    }

    public static T FromJson<T>(this String json, Newtonsoft.Json.JsonSerializerSettings settings)
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, settings);
    }

    public static T GetValue<T>(this String s)
    {
        MethodInfo mi = typeof(T).GetMethod("Parse", new Type[] { typeof(String) });
        if (s == default)
        {
            throw new ArgumentNullException("s");
        }
        else if (mi != null)
        {
            return (T)mi.Invoke(typeof(T), new object[] { s });
        }
        else if (typeof(T).IsEnum)
        {
            if (System.Enum.TryParse(typeof(T), s, out object ev))
                return (T)(object)ev;
            else
                throw new ArgumentException($"{s} is not a valid member of {typeof(T).Name}");
        }
        else
        {
            throw new ArgumentException($"No conversion supported for {typeof(T).Name}");
        }
    }

    public static T GetValue<T>(this String s, T defaultValue)
    {
        MethodInfo mi = typeof(T).GetMethod("TryParse", new Type[] { typeof(String), typeof(T).MakeByRefType() });
        if (s == default)
        {
            return defaultValue;
        }
        else if (mi != null)
        {
            T v;
            var p = new object[] { s, null };
            if ((bool)mi.Invoke(null, p))
                return (T)p[1];
            else
                return defaultValue;
        }
        else if (typeof(T).IsEnum && System.Enum.TryParse(typeof(T), s, out object ev))
        {
            return (T)ev;
        }
        else
        {
            return defaultValue;
        }
    }

    public static bool Is<TValue>(this String value)
    {
        System.ComponentModel.TypeConverter converter = System.ComponentModel.TypeDescriptor.GetConverter(typeof(TValue));
        if (converter != null)
        {
            try
            {
                if (value != null)
                {
                    if (!converter.CanConvertFrom((System.ComponentModel.ITypeDescriptorContext)null, value.GetType()))
                    {
                        return false;
                    }
                }
                converter.ConvertFrom((System.ComponentModel.ITypeDescriptorContext)null, System.Globalization.CultureInfo.CurrentCulture, (object)value);
                return true;
            }
            catch
            {
            }
        }
        return false;
    }

    public static T JsonToObject<T>(this String json)
    {
        var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        return JsonConvert.DeserializeObject<T>(json, settings);
    }

    public static IEnumerable<T> SplitTo<T>(this String str, params char[] separator) where T : IConvertible
    {
        return str.Split(separator, StringSplitOptions.None).Select(s => (T)System.Convert.ChangeType(s, typeof(T)));
    }

    public static IEnumerable<T> SplitTo<T>(this String str, StringSplitOptions options, params char[] separator)
        where T : IConvertible
    {
        return str.Split(separator, options).Select(s => (T)System.Convert.ChangeType(s, typeof(T)));
    }

    public static String ToDelimitedString<T>(this IEnumerable<T>? collection, String delimiter = ", ", String? endDelimiter = null)
    {
        if (collection is null)
            return String.Empty;

        var sb = new StringBuilder();
        using (var enumerator = collection.GetEnumerator())
        {
            if (enumerator.MoveNext())
            {
                sb.Append(enumerator.Current);
            }

            if (endDelimiter == default)
            {
                while (enumerator.MoveNext())
                {
                    sb.Append(delimiter + enumerator.Current);
                }
            }
            else
            {
                var hasNextValue = enumerator.MoveNext();
                while (hasNextValue)
                {
                    var current = enumerator.Current;
                    hasNextValue = enumerator.MoveNext();
                    if (!hasNextValue)
                    {
                        sb.Append(endDelimiter + current);
                        break;
                    }

                    sb.Append(delimiter + current);
                }
            }
        }

        return sb.ToString();
    }

    public static String ToDelimitedString<T>(this IEnumerable<T>? collection, Func<T, String> func, String delimiter = ", ", String? endDelimiter = null)
    {
        if (collection is null)
            return String.Empty;

        var sb = new StringBuilder();
        using (var enumerator = collection.GetEnumerator())
        {
            if (enumerator.MoveNext())
            {
                sb.Append(func.Invoke(enumerator.Current));
            }

            if (endDelimiter == default)
            {
                while (enumerator.MoveNext())
                {
                    sb.Append(delimiter + func.Invoke(enumerator.Current));
                }
            }
            else
            {
                var hasNextValue = enumerator.MoveNext();
                while (hasNextValue)
                {
                    var current = enumerator.Current;
                    hasNextValue = enumerator.MoveNext();
                    if (!hasNextValue)
                    {
                        sb.Append(endDelimiter + func.Invoke(current));
                        break;
                    }

                    sb.Append(delimiter + func.Invoke(current));
                }
            }
        }

        return sb.ToString();
    }

    public static TEnum ToEnum<TEnum>(this String text) where TEnum : struct
    {
        var enumType = typeof(TEnum);
        if (!enumType.GetTypeInfo().IsEnum) throw new ArgumentException("{0} is not an Enum".ToFormat(enumType.Name));
        return (TEnum)System.Enum.Parse(enumType, text, true);
    }

    public static T ToEnum<T>(this String value, T defaultValue = default(T)) where T : struct
    {
        if (!typeof(T).IsEnum)
        {
            throw new ArgumentException("Type T Must of type System.Enum");
        }

        T result;
        bool isParsed = System.Enum.TryParse(value, true, out result);
        return isParsed ? result : defaultValue;
    }

    public static TEnum? ToEnum<TEnum>(this String value, bool ignoreCase = false) where TEnum : struct
    {
        if (!typeof(TEnum).IsEnum)
            throw new ArgumentException($"Type {nameof(TEnum)} must be of type {typeof(System.Enum).FullName}");

        bool isValid = System.Enum.TryParse(value, ignoreCase, out TEnum result);

        if (!isValid)
            return null;

        return result;
    }

    public static String ToJson<T>(this T value, JsonSerializerSettings settings = null)
    {
        if (settings == default)
            settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

        return JsonConvert.SerializeObject(value, settings);
    }
}