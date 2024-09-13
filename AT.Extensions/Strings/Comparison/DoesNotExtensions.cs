﻿namespace AT.Extensions.Strings.Comparison;
public static class DoesNotExtensions : Object
{
    public static Boolean DoesNotEndWith(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        return value == default || suffix == default ||
               !value.EndsWith(suffix, StringComparison.InvariantCulture);
    }

    public static Boolean DoesNotStartWith(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        return value == default || prefix == default ||
               !value.StartsWith(prefix, StringComparison.InvariantCulture);
    }
}