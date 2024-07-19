namespace AT.Extensions.FileInfos.Boundary;
public static class Extensions : Object
{
    public static async Task CheckFileAccessAsync(this FileInfo file, Int32 timeout = 1000, Int32 iterationCount = 100, CancellationToken cancel = default)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        file.ThrowIfNotFound();
        for (Int32 i = 0; i < iterationCount; i++)
            try
            {
                cancel.ThrowIfCancellationRequested();
                using var stream = file.Open(FileMode.Open, FileAccess.Read);
                if (stream.Length > 0)
                    return;
            }
            catch (IOException)
            {
                await Task.Delay(timeout, cancel).ConfigureAwait(false);
            }
        cancel.ThrowIfCancellationRequested();
        // ----------------------------------------------------------------------------------------------------
        throw new InvalidOperationException($"File {file.FullName} is locked by another process");
    }

    public static FileInfo ThrowIfNotFound(this FileInfo file, String? Message = default)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        file.Refresh();
        return file.Exists
               ? file
               : throw new FileNotFoundException(Message ?? $"File '{file}' not found");
    }
}