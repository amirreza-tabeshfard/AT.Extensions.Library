using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DirectoryInfos.Mover;
public static class Extensions : Object
{
    public static Boolean MoveDirectory(this String sourceFullFilepath, String destinationFullFilepath)
    {
        if (sourceFullFilepath.IsNullOrEmpty() || sourceFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(sourceFullFilepath));
        else if (destinationFullFilepath.IsNullOrEmpty() || destinationFullFilepath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(destinationFullFilepath));
        // ----------------------------------------------------------------------------------------------------
        Boolean result;
        try
        {
            Directory.Move(sourceFullFilepath, destinationFullFilepath);
            result = true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return result;
    }
}
