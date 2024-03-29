﻿using AT.Extensions.DirectoryInfos.Creation;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DirectoryInfos.Deleted;
public static class Extensions
{
    public static void DeleteDirectory(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        if (directoryInfo.Parent is not null)
            directoryInfo.Parent.DeleteDirectory();

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }

    public static void DeleteDirectory(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        DirectoryInfo directoryInfo = new(path);
        // ----------------------------------------------------------------------------------------------------
        if (directoryInfo.Parent is not null)
            directoryInfo.Parent.CreateDirectory();

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }
}