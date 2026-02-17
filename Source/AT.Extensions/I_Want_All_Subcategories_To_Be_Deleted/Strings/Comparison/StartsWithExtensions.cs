namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static partial class StartsWithExtensions
{
    #region Method(s): Private

    private class CaseSensitiveEqualityComparer : IEqualityComparer<Char>
    {
        public Boolean Equals(Char c1, Char c2)
        {
            return c1.Equals(c2);
        }

        public Int32 GetHashCode([System.Diagnostics.CodeAnalysis.DisallowNull] Char obj)
        {
            return obj.GetHashCode();
        }
    }

    private class CaseInsensitiveEqualityComparer : IEqualityComparer<Char>
    {
        public Boolean Equals(Char c1, Char c2)
        {
            return Char.ToUpper(c1).Equals(Char.ToUpper(c2));
        }

        public Int32 GetHashCode([System.Diagnostics.CodeAnalysis.DisallowNull] Char obj)
        {
            return Char.ToUpper(obj).GetHashCode();
        }
    }

    private class CaseSensitiveComparer : IComparer<Char>
    {
        public Int32 Compare(Char c1, Char c2)
        {
            return c1.CompareTo(c2);
        }
    }

    private class CaseInsensitiveComparer : IComparer<Char>
    {
        public Int32 Compare(Char c1, Char c2)
        {
            return Char.ToUpper(c1).CompareTo(Char.ToUpper(c2));
        }
    }

    #endregion

    #region Method(s): internal

    internal static class CharComparer
    {
        public static IEqualityComparer<Char> GetEqualityComparer(Boolean ignoreCase) => ignoreCase ?
            new CaseInsensitiveEqualityComparer() :
            new CaseSensitiveEqualityComparer();

        public static IComparer<Char> GetComparer(Boolean ignoreCase) => ignoreCase ?
            new CaseInsensitiveComparer() :
            new CaseSensitiveComparer();
    }

    #endregion

    public static Boolean StartsWithIgnoreCase(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length < prefix.Length)
            return false;
        // ----------------------------------------------------------------------------------------------------
        return value.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean StartsWithInvariant(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        if (value != null && prefix != null)
            return value.StartsWith(prefix, StringComparison.Ordinal);
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean StartsWithInvariant(this String value, String prefix1, String prefix2)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix1);
        ArgumentException.ThrowIfNullOrEmpty(prefix2);
        // ----------------------------------------------------------------------------------------------------
        return value.StartsWithInvariant(prefix1) || value.StartsWithInvariant(prefix2);
    }

    public static Boolean StartsWithInvariant(this String value, String prefix1, String prefix2, String prefix3)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix1);
        ArgumentException.ThrowIfNullOrEmpty(prefix2);
        ArgumentException.ThrowIfNullOrEmpty(prefix3);
        // ----------------------------------------------------------------------------------------------------
        return value.StartsWithInvariant(prefix1, prefix2) || value.StartsWithInvariant(prefix3);
    }

    public static Boolean StartsWithInvariantIgnoreCase(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        if (value != null && prefix != null)
            return value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}