namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DirectoryInfos.Collections;
public static class EnumerateExtensions
{
    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateDirectories(null, new String[] { });
    }

    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateDirectories(searchOption, Array.Empty<String>());
    }

    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        List<DirectoryInfo>? result = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
            result.AddRange(directoryInfo.EnumerateDirectories());
        else if (searchOption is null && patterns.Length != 0)
            foreach (String pattern in patterns)
                result.AddRange(directoryInfo.EnumerateDirectories(pattern));
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
                result.AddRange(directoryInfo.EnumerateDirectories(pattern, searchOption.Value));
        // ----------------------------------------------------------------------------------------------------
        if (result?.Any() ?? false)
            return result;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}