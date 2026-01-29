namespace AT.Extensions.FileInfos.Process;
public static class ShowInExtensions
{
    public static System.Diagnostics.Process? ShowInExplorer(this FileSystemInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start("explorer", $"/select,\"{file.FullName}\"");
    }
}