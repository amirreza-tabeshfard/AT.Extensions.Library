using AT.Extensions.Strings.Collections.Generic;

namespace AT.Extensions.Strings.Extraction;
public static class AsExtensions : Object
{
    public static Decimal AsDecimal(this String value)
    {
        return value.As<Decimal>();
    }

    public static Decimal AsDecimal(this String value, Decimal defaultValue)
    {
        decimal result;
        if (!decimal.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static Double AsDouble(this String value)
    {
        return value.As<Double>();
    }

    public static Double AsDouble(this String value, Double defaultValue)
    {
        Double result;
        if (!Double.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static float AsFloat(this String value)
    {
        return value.AsFloat(0.0f);
    }

    public static float AsFloat(this String value, float defaultValue)
    {
        float result;
        if (!float.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static Single AsSingle(this String value)
    {
        return value.As<Single>();
    }

    public static Single AsSingle(this String value, Single defaultValue)
    {
        Single result;
        if (!Single.TryParse(value, out result))
            return defaultValue;
        return result;
    }
}