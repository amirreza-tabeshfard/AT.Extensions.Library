namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Writer;
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