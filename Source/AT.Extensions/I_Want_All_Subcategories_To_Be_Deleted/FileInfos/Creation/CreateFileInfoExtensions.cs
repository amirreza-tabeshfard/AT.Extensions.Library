namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Creation;
public static class CreateFileInfoExtensions
{
    public static FileInfo CreateFileInfo(this DirectoryInfo directory, String fileRelativePath)
    {
        ArgumentNullException.ThrowIfNull(directory);
        ArgumentNullException.ThrowIfNull(fileRelativePath);
        // ----------------------------------------------------------------------------------------------------
        return new(Path.Combine(directory.FullName, fileRelativePath.Replace(':', '.')));
    }
}