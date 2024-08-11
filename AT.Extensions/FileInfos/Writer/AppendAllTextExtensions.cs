namespace AT.Extensions.FileInfos.Writer;
public static class AppendAllTextExtensions : Object
{
    public static void AppendAllText(this FileInfo file, String text)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.Write(text);
    }

    public static void AppendAllText(this FileInfo file, byte[] buffer)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.Write(buffer);
    }
}