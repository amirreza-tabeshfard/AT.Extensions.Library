namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Process;
public static class ExecuteAsAdminExtensions
{
    public static System.Diagnostics.Process? ExecuteAsAdmin(this FileInfo file, String args = "", Boolean isUseShellExecute = true)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return file.Execute(args, isUseShellExecute, "runas");
    }
}