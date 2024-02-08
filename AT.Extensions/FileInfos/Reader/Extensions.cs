namespace AT.Extensions.FileInfos.Reader;
public static class Extensions : Object
{
    public static BinaryReader OpenBinaryReader(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(file.OpenRead());
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, int bufferLength)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferLength));
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, System.Text.Encoding encoding)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(file.OpenRead(), encoding);
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, int bufferLength, System.Text.Encoding encoding)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferLength), encoding);
    }

    public static string? ReadAllText(this String filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException(nameof(filePath));
        else if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return System.IO.File.ReadAllText(filePath);
    }

    public static string? ReadAllText(this String filePath, System.Text.Encoding encoding)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException(nameof(filePath));
        else if (!System.IO.File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return System.IO.File.ReadAllText(filePath, encoding);
    }

    public static string? ReadAllText(this FileInfo file, bool throwNotExist = true)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        else if (!file.Exists && !throwNotExist)
            return default;
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.OpenText();
        // ----------------------------------------------------------------------------------------------------
        return reader.ReadToEnd();
    }
}