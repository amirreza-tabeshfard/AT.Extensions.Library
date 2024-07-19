using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Creation;
public static class Extensions : Object
{
    public static BinaryWriter CreateBinary(this FileInfo file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(file.Create());
    }

    public static BinaryWriter CreateBinary(this FileInfo file, Int32 bufferLength)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, bufferLength));
    }

    public static BinaryWriter CreateBinary(this FileInfo file, System.Text.Encoding encoding)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(file.Create(), encoding);
    }

    public static BinaryWriter CreateBinary(this FileInfo file, Int32 bufferLength, System.Text.Encoding encoding)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, bufferLength), encoding);
    }

    public static FileStream? CreateFile(this String fullFilepath)
    {
        if (fullFilepath.IsNullOrEmpty() || fullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(fullFilepath));
        else if (!File.Exists(fullFilepath))
            throw new FileNotFoundException(fullFilepath);
        // ----------------------------------------------------------------------------------------------------
        using FileStream? result = File.Create(fullFilepath);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static FileStream? CreateFile(this String fullFilepath, Int32 bufferSize)
    {
        if (fullFilepath.IsNullOrEmpty() || fullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(fullFilepath));
        else if (!File.Exists(fullFilepath))
            throw new FileNotFoundException(fullFilepath);
        // ----------------------------------------------------------------------------------------------------
        using FileStream? result = File.Create(fullFilepath, bufferSize);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static FileStream CreateFile(this String fullFilepath, Int32 bufferSize, FileOptions options)
    {
        if (fullFilepath.IsNullOrEmpty() || fullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(fullFilepath));
        else if (!File.Exists(fullFilepath))
            throw new FileNotFoundException(fullFilepath);
        // ----------------------------------------------------------------------------------------------------
        using FileStream? result = File.Create(fullFilepath, bufferSize, options);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static FileInfo CreateFileInfo(this DirectoryInfo directory, String fileRelativePath)
    {
        if (directory is null)
            throw new ArgumentNullException(nameof(directory));
        // ----------------------------------------------------------------------------------------------------
        return new(Path.Combine(directory.FullName, fileRelativePath.Replace(':', '.')));
    }
}