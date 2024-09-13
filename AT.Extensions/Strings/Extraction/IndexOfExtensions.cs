﻿namespace AT.Extensions.Strings.Extraction;
public static class IndexOfExtensions : Object
{
    public static Int32 IndexOfInvariant(this String value, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.SafeIndexOf(substring, StringComparison.Ordinal);
    }

    public static Int32 IndexOfInvariant(this String value, String substring, Int32 startIndex)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.IndexOf(substring, startIndex, StringComparison.Ordinal);
    }

    public static Int32 IndexOfInvariantIgnoreCase(this String value, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.SafeIndexOf(substring, StringComparison.OrdinalIgnoreCase);
    }
}