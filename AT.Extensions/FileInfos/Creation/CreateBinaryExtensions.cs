namespace AT.Extensions.FileInfos.Creation;
public static class CreateBinaryExtensions
{
    public static BinaryWriter CreateBinary(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(file.Create());
    }

    public static BinaryWriter CreateBinary(this FileInfo file, Int32 bufferLength)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, bufferLength));
    }

    public static BinaryWriter CreateBinary(this FileInfo file, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(file.Create(), encoding);
    }

    public static BinaryWriter CreateBinary(this FileInfo file, Int32 bufferLength, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return new(new FileStream(file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, bufferLength), encoding);
    }
}