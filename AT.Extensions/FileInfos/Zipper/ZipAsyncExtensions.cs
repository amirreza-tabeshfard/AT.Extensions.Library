using AT.Extensions.FileInfos.Boundary;
using AT.Extensions.FileInfos.Copy;
using AT.Extensions.FileInfos.Creation;

namespace AT.Extensions.FileInfos.Zipper;
public static class ZipAsyncExtensions
{
    public static async Task<FileInfo> ZipAsync(this FileInfo file, byte[] buffer, String? archiveFileName = null, Boolean isOverride = true, IProgress<Double>? progress = null, CancellationToken cancel = default)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        file.ThrowIfNotFound();
        cancel.ThrowIfCancellationRequested();
        // ----------------------------------------------------------------------------------------------------
        if (archiveFileName is null)
            archiveFileName = $"{file.FullName}.zip";
        else if (!Path.IsPathRooted(archiveFileName))
            archiveFileName = (file.Directory ?? throw new InvalidOperationException($"Failed to get file directory {file}")).CreateFileInfo(archiveFileName).FullName;
        // ----------------------------------------------------------------------------------------------------
        using FileStream zipStream = File.Open(archiveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        using System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(zipStream);
        // ----------------------------------------------------------------------------------------------------
        System.IO.Compression.ZipArchiveEntry? fileEntry = zip.GetEntry(file.Name);
        if (fileEntry != null)
        {
            if (!isOverride)
                return new FileInfo(archiveFileName);

            fileEntry.Delete();
        }
        // ----------------------------------------------------------------------------------------------------
        using Stream fileEntryStream = zip.CreateEntry(file.Name).Open();
        using FileStream fileStream = file.OpenRead();
        // ----------------------------------------------------------------------------------------------------
        if (progress is null)
            await fileStream.CopyToAsync(fileEntryStream, buffer, cancel).ConfigureAwait(false);
        else
            await fileStream.CopyToAsync(fileEntryStream, buffer, fileStream.Length, progress, cancel).ConfigureAwait(false);
        // ----------------------------------------------------------------------------------------------------
        return new FileInfo(archiveFileName);
    }
}