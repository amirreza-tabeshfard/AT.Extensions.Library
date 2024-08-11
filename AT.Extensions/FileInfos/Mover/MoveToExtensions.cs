﻿using AT.Extensions.FileInfos.Creation;

namespace AT.Extensions.FileInfos.Mover;
public static class MoveToExtensions : Object
{
    public static FileInfo MoveTo(this FileInfo sourceFile, FileInfo destinationFile, Boolean isOverride = true)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationFile);
        // ----------------------------------------------------------------------------------------------------
        destinationFile.Refresh();

        if (destinationFile.Exists && !isOverride)
            return destinationFile;

        sourceFile.MoveTo(destinationFile.FullName);
        destinationFile.Refresh();
        sourceFile.Refresh();
        // ----------------------------------------------------------------------------------------------------
        return destinationFile;
    }

    public static FileInfo MoveTo(this FileInfo sourceFile, DirectoryInfo destinationInfo, Boolean isOverride = false)
    {
        ArgumentNullException.ThrowIfNull(sourceFile);
        ArgumentNullException.ThrowIfNull(destinationInfo);
        // ----------------------------------------------------------------------------------------------------
        FileInfo destinationFile = destinationInfo.CreateFileInfo(sourceFile.Name);

        if (destinationFile.Exists)
            if (isOverride)
                destinationFile.Delete();
            else
                return destinationFile;

        sourceFile.MoveTo(destinationFile);
        destinationFile.Refresh();
        sourceFile.Refresh();
        // ----------------------------------------------------------------------------------------------------
        return destinationFile;
    }
}