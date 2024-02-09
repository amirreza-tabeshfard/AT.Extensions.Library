namespace AT.Extensions.FileInfos.Writer;
public static class Extensions : Object
{
    public static FileStream Append(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return file.Open(FileMode.Append, FileAccess.Write);
    }

    public static void AppendAllBytes(this FileInfo file, byte[] data)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using FileStream fileStream = new(file.FullName, FileMode.Append, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
    }

    public static void AppendAllText(this FileInfo file, String text)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.Write(text);
    }

    public static void AppendAllText(this FileInfo file, byte[] buffer)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.Write(buffer);
    }

    public static void AppendLine(this FileInfo file, String text)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using StreamWriter writer = new(file.Open(FileMode.Append, FileAccess.Write, FileShare.Read));
        writer.WriteLine(text);
    }

    public static void CreateAllBytes(this FileInfo file, byte[] data)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        using FileStream fileStream = new(file.FullName, FileMode.Create, FileAccess.Write, FileShare.Read);
        fileStream.Write(data, 0, data.Length);
    }
}