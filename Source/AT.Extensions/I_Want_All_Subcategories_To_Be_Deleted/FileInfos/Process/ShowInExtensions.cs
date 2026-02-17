namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Process;
public static class ShowInExtensions
{
    public static System.Diagnostics.Process? ShowInExplorer(this FileSystemInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start("explorer", $"/select,\"{file.FullName}\"");
    }
}