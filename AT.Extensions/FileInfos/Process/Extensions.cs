namespace AT.Extensions.FileInfos.Process;
public static class Extensions : Object
{
    #region Method(s): Private

    private static T NotNull<T>(this T? obj, string? Message = null)
        where T : class
    {
        return obj ?? throw new InvalidOperationException(Message ?? "Empty object reference");
    }

    #endregion

    public static System.Diagnostics.Process? ExecuteAsAdmin(this FileInfo file, string args = "", bool isUseShellExecute = true)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return file.Execute(args, isUseShellExecute, "runas");
    }

    public static System.Diagnostics.Process? Execute(this FileInfo file, string args, bool isUseShellExecute, string verb)
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

    public static System.Diagnostics.Process Execute(string file, string args = "", bool isUseShellExecute = true)
    {
        if (string.IsNullOrEmpty(file))
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file, args) { UseShellExecute = isUseShellExecute }).NotNull();
    }

    public static System.Diagnostics.Process Execute(this FileInfo file, string args = "", bool isUseShellExecute = true)
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