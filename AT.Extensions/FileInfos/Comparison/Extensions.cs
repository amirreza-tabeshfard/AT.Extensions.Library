using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Comparison;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly HashSet<char> _NotValidPathChars;

    #endregion

    #region Constructor

    static Extensions()
    {
        _NotValidPathChars = new(Path.GetInvalidFileNameChars());
    }

    #endregion

    public static bool IsEOF(this BinaryReader reader)
    {
        if (reader == default)
            throw new ArgumentNullException(nameof(reader));
        // ----------------------------------------------------------------------------------------------------
        return reader.BaseStream.Position == reader.BaseStream.Length;
    }

    public static bool IsFileExists(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        if (File.Exists(path))
            return true;
        // ----------------------------------------------------------------------------------------------------
        return false;
    }

    public static bool IsMoveFile(this String sourceFullFilepath, String destinationFullFilepath)
    {
        if (sourceFullFilepath.IsNullOrEmpty() || sourceFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(sourceFullFilepath));
        else if (destinationFullFilepath.IsNullOrEmpty() || destinationFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(destinationFullFilepath));
        // ----------------------------------------------------------------------------------------------------
        bool isSuccess = default;
        try
        {
            File.Move(sourceFullFilepath, destinationFullFilepath);
            isSuccess = true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
        // ----------------------------------------------------------------------------------------------------
        return isSuccess;
    }

    public static bool IsValidFileName(this String name)
    {
        if (name.IsNullOrEmpty() || name.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(name));
        // ----------------------------------------------------------------------------------------------------
        return !String.IsNullOrWhiteSpace(name)
               && !name.Trim(' ').Any(_NotValidPathChars.Contains);
    }
}