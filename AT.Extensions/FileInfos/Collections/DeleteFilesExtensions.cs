namespace AT.Extensions.FileInfos.Collections;
public static class DeleteFilesExtensions
{
    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, out Int32 count)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.DeleteFiles(null, out count, Array.Empty<String>());
    }

    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, out Int32 count)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        return directoryInfo.DeleteFiles(searchOption, out count, Array.Empty<String>());
    }

    public static List<Dictionary<FileInfo, Boolean>>? DeleteFiles(this DirectoryInfo directoryInfo, SearchOption? searchOption, out Int32 count, params String[] patterns)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        Dictionary<FileInfo, Boolean>? keyValues;
        List<Dictionary<FileInfo, Boolean>>? result = default;
        FileInfo[]? getFiles;
        List<FileInfo>? files = new();
        count = default;
        // ----------------------------------------------------------------------------------------------------
        if (searchOption is null && patterns.Length == 0)
        {
            getFiles = directoryInfo.GetFiles();
            files.AddRange(getFiles);
        }
        else if (searchOption is null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        else if (searchOption is not null && patterns.Length != 0)
            foreach (String pattern in patterns)
            {
                getFiles = directoryInfo.GetFiles(searchPattern: pattern, searchOption: searchOption.Value);
                if (getFiles is not null)
                    if (getFiles.Any())
                        files.AddRange(getFiles);
            }
        // ----------------------------------------------------------------------------------------------------
        if (files is not null)
            if (files.Any())
            {
                keyValues = new();
                result = new();
                foreach (FileInfo fileInfo in files)
                {
                    keyValues.Add(fileInfo, true);
                    result.Add(keyValues);
                    fileInfo.Delete();
                    count++;
                }
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}