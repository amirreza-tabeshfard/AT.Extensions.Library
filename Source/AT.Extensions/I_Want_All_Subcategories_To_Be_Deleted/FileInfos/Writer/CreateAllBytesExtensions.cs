namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Writer;
public static class CreateAllBytesExtensions
{
    public static void CreateAllBytes(this FileInfo file, byte[] data)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using FileStream fileStream = new(file.FullName, FileMode.Create, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
    }
}