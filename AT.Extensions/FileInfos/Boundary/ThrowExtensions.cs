namespace AT.Extensions.FileInfos.Boundary;
public static class ThrowExtensions : Object
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