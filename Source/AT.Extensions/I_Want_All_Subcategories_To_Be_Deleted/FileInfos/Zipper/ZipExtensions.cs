using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Boundary;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Creation;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Zipper;
public static class ZipExtensions
{
    public static FileInfo Zip(this FileInfo file, String? archiveFileName = null, Boolean isOverride = true)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        file.ThrowIfNotFound();
        // ----------------------------------------------------------------------------------------------------
        if (archiveFileName is null)
            archiveFileName = $"{file.FullName}.zip";
        else if (!Path.IsPathRooted(archiveFileName))
            archiveFileName = (file.Directory ?? throw new InvalidOperationException($"File directory '{file}' not received")).CreateFileInfo(archiveFileName).FullName;
        // ----------------------------------------------------------------------------------------------------
        using FileStream zipStream = File.Open(archiveFileName, FileMode.OpenOrCreate, FileAccess.Write);
        using System.IO.Compression.ZipArchive zip = new System.IO.Compression.ZipArchive(zipStream);
        // ----------------------------------------------------------------------------------------------------
        System.IO.Compression.ZipArchiveEntry? fileEntry = zip.GetEntry(file.Name);
        // ----------------------------------------------------------------------------------------------------
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
        fileStream.CopyTo(fileEntryStream);
        // ----------------------------------------------------------------------------------------------------
        return new FileInfo(archiveFileName);
    }
}