namespace AT.Extensions.FileInfos.Copy;
public static class CopyToAsyncExtensions : Object
{
    public static Task CopyToAsync(this Stream input, Stream output, Int32 bufferLength = 0x1000, CancellationToken cancel = default)
    {
        return bufferLength < 1
               ? throw new ArgumentOutOfRangeException(nameof(bufferLength), "Copy buffer length less than one byte")
               : input.CopyToAsync(output, new byte[bufferLength], cancel);
    }

    public static async Task CopyToAsync(this Stream input, Stream output, byte[] buffer, CancellationToken Cancel = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(buffer);
        // ----------------------------------------------------------------------------------------------------
        if (!input.CanRead)
            throw new ArgumentException("Input stream is not readable", nameof(input));

        if (!output.CanWrite)
            throw new ArgumentException("Output stream is not writable", nameof(output));

        if (buffer.Length == 0)
            throw new ArgumentException("Copy buffer size is 0", nameof(buffer));
        // ----------------------------------------------------------------------------------------------------
        Int32 bufferLength = buffer.Length;
        Int32 readed;
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

    public static async Task CopyToAsync(this Stream input, Stream output, byte[] buffer, Int64 length, IProgress<Double>? progress = null, CancellationToken cancel = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentNullException.ThrowIfNull(output);
        ArgumentNullException.ThrowIfNull(buffer);
        // ----------------------------------------------------------------------------------------------------
        if (!input.CanRead)
            throw new ArgumentException("Input stream is not readable", nameof(input));

        if (!output.CanWrite)
            throw new ArgumentException("Output stream is not writable", nameof(output));

        if (buffer.Length == 0)
            throw new ArgumentException("Copy buffer size is 0", nameof(buffer));
        // ----------------------------------------------------------------------------------------------------
        Int32 readed;
        Int32 totalReaded = 0;
        Double lastPercent = 0d;
        do
        {
            cancel.ThrowIfCancellationRequested();
            readed = await input
                     .ReadAsync(buffer, 0, (Int32)Math.Min(buffer.Length, length - totalReaded), cancel)
                     .ConfigureAwait(false);

            if (readed == 0)
                continue;

            totalReaded += readed;
            cancel.ThrowIfCancellationRequested();
            await output.WriteAsync(buffer, 0, readed, cancel).ConfigureAwait(false);
            Double percent = (Double)totalReaded / length;
            if (percent - lastPercent >= 0.01)
                progress?.Report(lastPercent = percent);
        } while (readed > 0 && totalReaded < length);
    }
}