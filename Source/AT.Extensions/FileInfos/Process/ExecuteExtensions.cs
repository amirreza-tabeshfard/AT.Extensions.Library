namespace AT.Extensions.FileInfos.Process;
public static class ExecuteExtensions
{
    #region Method(s): Private

    private static T NotNull<T>(this T? obj, String? Message = null)
        where T : class
    {
        return obj ?? throw new InvalidOperationException(Message ?? "Empty object reference");
    }

    #endregion

    public static System.Diagnostics.Process? Execute(this FileInfo file, String args, Boolean isUseShellExecute, String verb)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(args);
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file.FullName, args)
        {
            UseShellExecute = isUseShellExecute,
            Verb = verb
        });
    }

    public static System.Diagnostics.Process Execute(this String file, String args = "", Boolean isUseShellExecute = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(file);
        ArgumentNullException.ThrowIfNull(args);
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(file, args) { UseShellExecute = isUseShellExecute }).NotNull();
    }

    public static System.Diagnostics.Process Execute(this FileInfo file, String args = "", Boolean isUseShellExecute = true)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(args);
        // ----------------------------------------------------------------------------------------------------
        return System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(isUseShellExecute ? file.ToString() : file.FullName, args) { UseShellExecute = isUseShellExecute }).NotNull();
    }
}