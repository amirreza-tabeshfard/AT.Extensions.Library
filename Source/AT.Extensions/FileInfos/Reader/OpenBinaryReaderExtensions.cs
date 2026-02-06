namespace AT.Extensions.FileInfos.Reader;
public static class OpenBinaryReaderExtensions
{
    public static BinaryReader OpenBinaryReader(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(file.OpenRead());
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, Int32 bufferLength)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferLength));
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(file.OpenRead(), encoding);
    }

    public static BinaryReader OpenBinaryReader(this FileInfo file, Int32 bufferLength, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferLength), encoding);
    }
}