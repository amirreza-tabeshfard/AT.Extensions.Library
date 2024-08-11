namespace AT.Extensions.FileInfos.Process;
public static class ExecuteAsAdminExtensions : Object
{
    public static System.Diagnostics.Process? ExecuteAsAdmin(this FileInfo file, String args = "", Boolean isUseShellExecute = true)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return file.Execute(args, isUseShellExecute, "runas");
    }
}