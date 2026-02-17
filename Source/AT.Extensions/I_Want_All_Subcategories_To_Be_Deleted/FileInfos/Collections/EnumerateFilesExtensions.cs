namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Collections;
public static class EnumerateFilesExtensions
{
    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateFiles(null, Array.Empty<String>());
    }

    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.EnumerateFiles(searchOption, Array.Empty<String>());
    }

    public static IEnumerable<FileInfo> EnumerateFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<FileInfo>? getFiles = default;
        List<FileInfo> result = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.EnumerateFiles();
            result.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
        {
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.EnumerateFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Count() != 0)
                        result.AddRange(getFiles);
            }
        }
        else if (searchOption is not null && patterns.Length != 0)
        {
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.EnumerateFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Count() != 0)
                        result.AddRange(getFiles);
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}