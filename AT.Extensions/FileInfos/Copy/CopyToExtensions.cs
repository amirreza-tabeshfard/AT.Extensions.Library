namespace AT.Extensions.FileInfos.Copy;
public static class CopyToExtensions : Object
{
    public static FileInfo CopyTo(this FileInfo sourceFile, DirectoryInfo destinationDirectory)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationDirectory);
        // ----------------------------------------------------------------------------------------------------
        return sourceFile.CopyTo(Path.Combine(destinationDirectory.FullName, Path.GetFileName(sourceFile.Name)));
    }

    public static FileInfo CopyTo(this FileInfo sourceFile, DirectoryInfo destinationDirectory, Boolean isOverwrite)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationDirectory);
        // ----------------------------------------------------------------------------------------------------
        String newFile = Path.Combine(destinationDirectory.FullName, Path.GetFileName(sourceFile.Name));
        // ----------------------------------------------------------------------------------------------------
        return !isOverwrite && File.Exists(newFile)
               ? new FileInfo(newFile)
               : sourceFile.CopyTo(newFile, true);
    }

    public static void CopyTo(this FileInfo sourceFile, FileInfo destinationFile)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationFile);
        // ----------------------------------------------------------------------------------------------------
        sourceFile.CopyTo(destinationFile.FullName);
    }

    public static void CopyTo(this FileInfo sourceFile, FileInfo destinationFile, Boolean isOverwrite)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationFile);
        // ----------------------------------------------------------------------------------------------------
        sourceFile.CopyTo(destinationFile.FullName, isOverwrite);
    }
}