namespace AT.Extensions.FileInfos.Writer;
public static class CreateAllBytesExtensions : Object
{
    public static void CreateAllBytes(this FileInfo file, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using FileStream fileStream = new(file.FullName, FileMode.Create, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
    }
}