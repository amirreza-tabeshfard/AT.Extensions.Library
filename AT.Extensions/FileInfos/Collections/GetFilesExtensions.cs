namespace AT.Extensions.FileInfos.Collections;
public static class GetFilesExtensions
{
    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetFiles(null, Array.Empty<String>());
    }

    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.GetFiles(searchOption, Array.Empty<String>());
    }

    public static FileInfo[]? GetFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, params String[] patterns)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        FileInfo[]? result = default;
        FileInfo[]? getFiles = default;
        List<FileInfo> files = new();
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.GetFiles();
            files.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
        {
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        }
        else if (searchOption is not null && patterns.Length != 0)
        {
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        }
        // ----------------------------------------------------------------------------------------------------
        if (files is not null)
            if (files.Any())
                result = files.ToArray();
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}