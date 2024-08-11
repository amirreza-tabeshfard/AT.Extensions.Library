namespace AT.Extensions.FileInfos.Reader;
public static class ReadAllTextExtensions : Object
{
    public static String? ReadAllText(this String filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllText(filePath);
    }

    public static String? ReadAllText(this String filePath, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
        // ----------------------------------------------------------------------------------------------------
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllText(filePath, encoding);
    }

    public static String? ReadAllText(this FileInfo file, Boolean throwNotExist = true)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        if (!file.Exists && !throwNotExist)
            return default;
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.OpenText();
        // ----------------------------------------------------------------------------------------------------
        return reader.ReadToEnd();
    }
}