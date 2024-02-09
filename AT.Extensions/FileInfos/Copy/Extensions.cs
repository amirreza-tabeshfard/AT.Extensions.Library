using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Copy;
public static class Extensions : Object
{
    public static FileInfo CopyTo(this FileInfo sourceFile, DirectoryInfo destinationDirectory)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationDirectory == default)
            throw new ArgumentNullException(nameof(destinationDirectory));
        // ----------------------------------------------------------------------------------------------------
        return sourceFile.CopyTo(Path.Combine(destinationDirectory.FullName, Path.GetFileName(sourceFile.Name)));
    }

    public static FileInfo CopyTo(this FileInfo sourceFile, DirectoryInfo destinationDirectory, bool isOverwrite)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationDirectory == default)
            throw new ArgumentNullException(nameof(destinationDirectory));
        // ----------------------------------------------------------------------------------------------------
        String newFile = Path.Combine(destinationDirectory.FullName, Path.GetFileName(sourceFile.Name));
        // ----------------------------------------------------------------------------------------------------
        return !isOverwrite && File.Exists(newFile)
               ? new FileInfo(newFile)
               : sourceFile.CopyTo(newFile, true);
    }

    public static void CopyTo(this FileInfo sourceFile, FileInfo destinationFile)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationFile == default)
            throw new ArgumentNullException(nameof(destinationFile));
        // ----------------------------------------------------------------------------------------------------
        sourceFile.CopyTo(destinationFile.FullName);
    }

    public static void CopyTo(this FileInfo sourceFile, FileInfo destinationFile, bool isOverwrite)
    {
        if (sourceFile == default)
            throw new ArgumentNullException(nameof(sourceFile));
        else if (destinationFile == default)
            throw new ArgumentNullException(nameof(destinationFile));
        // ----------------------------------------------------------------------------------------------------
        sourceFile.CopyTo(destinationFile.FullName, isOverwrite);
    }

    public static Task CopyToAsync(this Stream input, Stream output, int bufferLength = 0x1000, CancellationToken cancel = default)
    {
        return bufferLength < 1
               ? throw new ArgumentOutOfRangeException(nameof(bufferLength), "Copy buffer length less than one byte")
               : input.CopyToAsync(output, new byte[bufferLength], cancel);
    }

    public static async Task CopyToAsync(this Stream input, Stream output, byte[] buffer, CancellationToken Cancel = default)
    {
        if (input == default)
            throw new ArgumentNullException(nameof(input));
        else if (!input.CanRead)
            throw new ArgumentException("Input stream is not readable", nameof(input));

        if (output == default)
            throw new ArgumentNullException(nameof(output));
        else if (!output.CanWrite)
            throw new ArgumentException("Output stream is not writable", nameof(output));

        if (buffer is null)
            throw new ArgumentNullException(nameof(buffer));
        else if (buffer.Length == 0)
            throw new ArgumentException("Copy buffer size is 0", nameof(buffer));
        // ----------------------------------------------------------------------------------------------------
        int bufferLength = buffer.Length;
        int readed;
        do
        {
            Cancel.ThrowIfCancellationRequested();
            readed = await input.ReadAsync(buffer, 0, bufferLength, Cancel).ConfigureAwait(false);
            if (readed == 0)
                continue;
            Cancel.ThrowIfCancellationRequested();
            await output.WriteAsync(buffer, 0, readed, Cancel).ConfigureAwait(false);
        } while (readed > 0);
    }

    public static async Task CopyToAsync(this Stream input, Stream output, byte[] buffer, long length, IProgress<double>? progress = null, CancellationToken cancel = default)
    {
        if (input == default)
            throw new ArgumentNullException(nameof(input));
        else if (!input.CanRead)
            throw new ArgumentException("Input stream is not readable", nameof(input));

        if (output == default)
            throw new ArgumentNullException(nameof(output));
        else if (!output.CanWrite)
            throw new ArgumentException("Output stream is not writable", nameof(output));

        if (buffer is null)
            throw new ArgumentNullException(nameof(buffer));
        else if (buffer.Length == 0)
            throw new ArgumentException("Copy buffer size is 0", nameof(buffer));
        // ----------------------------------------------------------------------------------------------------
        int readed;
        int totalReaded = 0;
        double lastPercent = 0d;
        do
        {
            cancel.ThrowIfCancellationRequested();
            readed = await input
                     .ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, length - totalReaded), cancel)
                     .ConfigureAwait(false);

            if (readed == 0)
                continue;

            totalReaded += readed;
            cancel.ThrowIfCancellationRequested();
            await output.WriteAsync(buffer, 0, readed, cancel).ConfigureAwait(false);
            double percent = (double)totalReaded / length;
            if (percent - lastPercent >= 0.01)
                progress?.Report(lastPercent = percent);
        } while (readed > 0 && totalReaded < length);
    }

    public static void CopyToFile(this System.Text.StringBuilder contents, String path)
    {
        if (contents == default)
            throw new ArgumentNullException(nameof(contents));
        // ----------------------------------------------------------------------------------------------------
        File.WriteAllText(path, contents.ToString());
    }

    public static void CopyToFile(this String contents, String path)
    {
        if (contents.IsNullOrEmpty() || contents.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(contents));
        // ----------------------------------------------------------------------------------------------------
        File.WriteAllText(path, contents);
    }
}