using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Process;
public static class Extensions : Object
{
    #region Method(s): Private

    private static T NotNull<T>(this T? obj, String? Message = null)
        where T : class
    {
        return obj ?? throw new InvalidOperationException(Message ?? "Empty object reference");
    }

    #endregion

    public static System.Diagnostics.Process? ExecuteAsAdmin(this FileInfo file, String args = "", bool isUseShellExecute = true)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return file.Execute(args, isUseShellExecute, "runas");
    }

    public static System.Diagnostics.Process? Execute(this FileInfo file, String args, bool isUseShellExecute, String verb)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file.FullName, args)
        {
            UseShellExecute = isUseShellExecute,
            Verb = verb
        });
    }

    public static System.Diagnostics.Process Execute(this String file, String args = "", bool isUseShellExecute = true)
    {
        if (file.IsNullOrEmpty() || file.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file, args) { UseShellExecute = isUseShellExecute }).NotNull();
    }

    public static System.Diagnostics.Process Execute(this FileInfo file, String args = "", bool isUseShellExecute = true)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(isUseShellExecute ? file.ToString() : file.FullName, args) { UseShellExecute = isUseShellExecute }).NotNull();
    }

    public static System.Diagnostics.Process? ShowInExplorer(this FileSystemInfo file)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start("explorer", $"/select,\"{file.FullName}\"");
    }
}