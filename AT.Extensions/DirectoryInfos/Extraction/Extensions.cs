﻿namespace AT.Extensions.DirectoryInfos.Extraction;
public static class Extensions : Object
{
    public static Int64 GetSize(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        Int64 length = directoryInfo
                      .GetFiles()
                      .Sum(nextfile => nextfile.Exists ? nextfile.Length : 0);

        length += directoryInfo
                  .GetDirectories()
                  .Sum(nextdir => nextdir.Exists ? nextdir.GetSize() : 0);
        // ----------------------------------------------------------------------------------------------------
        return length;
    }
}