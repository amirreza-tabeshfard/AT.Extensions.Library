namespace AT.Extensions.DirectoryInfos.Collections;
public static class GetExtensions : Object
{
    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetDirectories(default, Array.Empty<String>());
    }

    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetDirectories(searchOption, Array.Empty<String>());
    }

    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        List<DirectoryInfo>? result = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
            result.AddRange(directoryInfo.GetDirectories());
        else if (searchOption is null && patterns.Length != 0)
            foreach (String pattern in patterns)
                result.AddRange(directoryInfo.GetDirectories(pattern));
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
                result.AddRange(directoryInfo.GetDirectories(pattern, searchOption.Value));
        // ----------------------------------------------------------------------------------------------------
        if (result?.Any() ?? false)
            return result.ToArray();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}