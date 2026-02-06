namespace AT.Extensions.FileInfos.Writer;
public static class AppendLineExtensions
{
    public static void AppendLine(this FileInfo file, String text)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.WriteLine(text);
    }
}