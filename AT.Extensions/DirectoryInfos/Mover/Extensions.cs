namespace AT.Extensions.DirectoryInfos.Mover;
public static class Extensions : Object
{
    public static bool MoveDirectory(this String sourceFullFilepath, string destinationFullFilepath)
    {
        if (string.IsNullOrEmpty(sourceFullFilepath))
            throw new ArgumentNullException(nameof(sourceFullFilepath));
        else if (string.IsNullOrEmpty(destinationFullFilepath))
            throw new ArgumentNullException(nameof(destinationFullFilepath));
        // ----------------------------------------------------------------------------------------------------
        bool result;
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
