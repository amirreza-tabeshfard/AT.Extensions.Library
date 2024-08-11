namespace AT.Extensions.FileInfos.Creation;
public static class CreateFileInfoExtensions : Object
{
    public static FileInfo CreateFileInfo(this DirectoryInfo directory, String fileRelativePath)
    {
        ArgumentNullException.ThrowIfNull(directory);
        ArgumentNullException.ThrowIfNull(fileRelativePath);
        // ----------------------------------------------------------------------------------------------------
        return new(Path.Combine(directory.FullName, fileRelativePath.Replace(':', '.')));
    }
}