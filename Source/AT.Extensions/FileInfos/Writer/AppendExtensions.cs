namespace AT.Extensions.FileInfos.Writer;
public static class AppendExtensions
{
    public static FileStream Append(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return file.Open(FileMode.Append, FileAccess.Write);
    }
}