using AT.Extensions.FileInfos.Boundary;
using AT.Extensions.FileInfos.Comparison;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Collections;
public static class ReadAllBytesExtensions : Object
{
    public static byte[] ReadAllBytes(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllBytes(filePath);
    }

    public static IEnumerable<byte>? ReadAllBytes(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using FileStream stream = file.ThrowIfNotFound().OpenRead();
        using BinaryReader reader = new(stream);
        while (!reader.IsEOF())
            yield return reader.ReadByte();
    }

    public static IEnumerable<byte[]>? ReadAllBytes(this FileInfo file, Int32 length)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using FileStream stream = file.ThrowIfNotFound().OpenRead();
        using BinaryReader reader = new BinaryReader(stream);
        while (!reader.IsEOF())
            yield return reader.ReadBytes(length);
    }
}