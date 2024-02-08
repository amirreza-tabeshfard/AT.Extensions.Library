using AT.Extensions.FileInfos.Boundary;
using AT.Extensions.FileInfos.Copy;
using AT.Extensions.FileInfos.Creation;

namespace AT.Extensions.FileInfos.Zipper;
public static class Extensions
{
    public static FileInfo Zip(this FileInfo file, string? archiveFileName = null, bool isOverride = true)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        file.ThrowIfNotFound();

        if (archiveFileName is null)
            archiveFileName = $"{file.FullName}.zip";
        else if (!Path.IsPathRooted(archiveFileName))
            archiveFileName = (file.Directory ?? throw new InvalidOperationException($"File directory '{file}' not received")).CreateFileInfo(archiveFileName).FullName;

        using FileStream zipStream = System.IO.File.Open(archiveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        using System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(zipStream);

        System.IO.Compression.ZipArchiveEntry? fileEntry = zip.GetEntry(file.Name);
        if (fileEntry != null)
        {
            if (!isOverride)
                return new FileInfo(archiveFileName);

            fileEntry.Delete();
        }

        using Stream fileEntryStream = zip.CreateEntry(file.Name).Open();
        using FileStream fileStream = file.OpenRead();
        fileStream.CopyTo(fileEntryStream);
        // ----------------------------------------------------------------------------------------------------
        return new FileInfo(archiveFileName);
    }

    public static async Task<FileInfo> ZipAsync(this FileInfo file, byte[] buffer, string? archiveFileName = null, bool isOverride = true, IProgress<double>? progress = null, CancellationToken cancel = default)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        file.ThrowIfNotFound();
        cancel.ThrowIfCancellationRequested();

        if (archiveFileName is null)
            archiveFileName = $"{file.FullName}.zip";
        else if (!Path.IsPathRooted(archiveFileName))
            archiveFileName = (file.Directory ?? throw new InvalidOperationException($"Failed to get file directory {file}")).CreateFileInfo(archiveFileName).FullName;

        using FileStream zipStream = System.IO.File.Open(archiveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        using System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(zipStream);

        System.IO.Compression.ZipArchiveEntry? fileEntry = zip.GetEntry(file.Name);
        if (fileEntry != null)
        {
            if (!isOverride)
                return new FileInfo(archiveFileName);

            fileEntry.Delete();
        }

        using Stream fileEntryStream = zip.CreateEntry(file.Name).Open();
        using FileStream fileStream = file.OpenRead();
        if (progress is null)
            await fileStream.CopyToAsync(fileEntryStream, buffer, cancel).ConfigureAwait(false);
        else
            await fileStream.CopyToAsync(fileEntryStream, buffer, fileStream.Length, progress, cancel).ConfigureAwait(false);
        // ----------------------------------------------------------------------------------------------------
        return new FileInfo(archiveFileName);
    }
}