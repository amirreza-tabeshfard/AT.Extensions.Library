using AT.Extensions.FileInfos.Boundary;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Collections;
public static class ReadAllLinesExtensions : Object
{
    public static String[] ReadAllLines(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath);
    }

    public static String[] ReadAllLines(this String filePath, System.Text.Encoding encoding)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath, encoding);
    }

    public static IEnumerable<String?>? ReadAllLines(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.ThrowIfNotFound().OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }

    public static IEnumerable<String?>? ReadAllLines(this FileInfo file, Action<StreamReader>? initializer, Int32 bufferSize = 3 * Infrastructure.DataLength.Bytes.MB)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = new(new BufferedStream(file.ThrowIfNotFound().Open(FileMode.Open, FileAccess.Read, FileShare.Read), bufferSize));
        initializer?.Invoke(reader);
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }
}