using AT.Extensions.FileInfos.Creation;

namespace AT.Extensions.FileInfos.Mover;
public static class Extensions : Object
{
    #region Method(s): Private

    private static T ParamNotNull<T>(this T? obj, String ParameterName, String? Message = null)
        where T : class
    {
        return obj ?? throw new ArgumentException(Message ?? $"Missing reference for parameter {ParameterName}", ParameterName);
    }

    #endregion

    public static FileInfo ChangeExtension(this FileInfo file, String? extension)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(Path.ChangeExtension(file.ParamNotNull(nameof(file)).FullName, extension));
    }

    public static FileInfo MoveTo(this FileInfo sourceFile, FileInfo destinationFile, bool isOverride = true)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationFile == default)
            throw new ArgumentNullException(nameof(destinationFile));
        // ----------------------------------------------------------------------------------------------------
        destinationFile.Refresh();

        if (destinationFile.Exists && !isOverride)
            return destinationFile;

        sourceFile.MoveTo(destinationFile.FullName);
        destinationFile.Refresh();
        sourceFile.Refresh();
        // ----------------------------------------------------------------------------------------------------
        return destinationFile;
    }

    public static FileInfo MoveTo(this FileInfo sourceFile, DirectoryInfo destinationInfo, bool isOverride = false)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationInfo == default)
            throw new ArgumentNullException(nameof(destinationInfo));
        // ----------------------------------------------------------------------------------------------------
        FileInfo destinationFile = destinationInfo.CreateFileInfo(sourceFile.Name);

        if (destinationFile.Exists)
            if (isOverride)
                destinationFile.Delete();
            else
                return destinationFile;

        sourceFile.MoveTo(destinationFile);
        destinationFile.Refresh();
        sourceFile.Refresh();
        // ----------------------------------------------------------------------------------------------------
        return destinationFile;
    }
}