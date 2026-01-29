using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Creation;
public static class CreateFileExtensions
{
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
}