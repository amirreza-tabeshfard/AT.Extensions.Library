namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Boundary;
public static class ThrowExtensions
{
    public static FileInfo ThrowIfNotFound(this FileInfo file, String? Message = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        file.Refresh();
        return file.Exists
               ? file
               : throw new FileNotFoundException(Message ?? $"File '{file}' not found");
    }
}