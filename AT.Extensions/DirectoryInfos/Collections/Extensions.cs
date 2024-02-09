namespace AT.Extensions.DirectoryInfos.Collections;
public static class Extensions : Object
{
    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateDirectories(null, new String[] { });
    }

    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateDirectories(searchOption, Array.Empty<String>());
    }

    public static IEnumerable<DirectoryInfo>? EnumerateDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
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
        if (result is not null)
            if (!result.Any())
                result = default;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetDirectories(null, Array.Empty<String>());
    }

    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetDirectories(searchOption, Array.Empty<String>());
    }

    public static DirectoryInfo[]? GetDirectories(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
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
        if (result is not null)
            if (!result.Any())
                return default;
            else
                return result.ToArray();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}