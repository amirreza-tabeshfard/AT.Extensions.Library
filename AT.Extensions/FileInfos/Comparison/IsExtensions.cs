using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Comparison;
public static class IsExtensions : Object
{
    #region Field(s)

    private static readonly HashSet<Char> _NotValidPathChars;

    #endregion

    #region Constructor

    static IsExtensions()
    {
        _NotValidPathChars = new(Path.GetInvalidFileNameChars());
    }

    #endregion

    public static Boolean IsEOF(this BinaryReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        // ----------------------------------------------------------------------------------------------------
        return reader.BaseStream.Position == reader.BaseStream.Length;
    }

    public static Boolean IsFileExists(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        if (File.Exists(path))
            return true;
        // ----------------------------------------------------------------------------------------------------
        return false;
    }

    public static Boolean IsMoveFile(this String sourceFullFilepath, String destinationFullFilepath)
    {
        if (sourceFullFilepath.IsNullOrEmpty() || sourceFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(sourceFullFilepath));
        else if (destinationFullFilepath.IsNullOrEmpty() || destinationFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(destinationFullFilepath));
        // ----------------------------------------------------------------------------------------------------
        Boolean isSuccess = default;
        try
        {
            File.Move(sourceFullFilepath, destinationFullFilepath);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        // ----------------------------------------------------------------------------------------------------
        return isSuccess;
    }

    public static Boolean IsValidFileName(this String name)
    {
        if (name.IsNullOrEmpty() || name.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(name));
        // ----------------------------------------------------------------------------------------------------
        return !String.IsNullOrWhiteSpace(name)
               && !name.Trim(' ').Any(_NotValidPathChars.Contains);
    }
}