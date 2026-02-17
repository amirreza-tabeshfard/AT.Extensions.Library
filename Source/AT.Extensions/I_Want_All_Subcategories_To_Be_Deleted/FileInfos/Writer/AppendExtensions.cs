namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Writer;
public static class AppendExtensions
{
    public static FileStream Append(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return file.Open(FileMode.Append, FileAccess.Write);
    }
}