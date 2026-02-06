namespace AT.Extensions.FileInfos.Writer;
public static class AppendAllBytesExtensions
{
    public static void AppendAllBytes(this FileInfo file, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using FileStream fileStream = new(file.FullName, FileMode.Append, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
    }
}