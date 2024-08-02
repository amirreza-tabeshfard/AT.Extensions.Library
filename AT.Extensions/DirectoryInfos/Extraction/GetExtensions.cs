namespace AT.Extensions.DirectoryInfos.Extraction;
public static class GetExtensions : Object
{
    public static Int64 GetSize(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        Int64 length = directoryInfo
                      .GetFiles()
                      .Sum(nextfile => nextfile.Exists ? nextfile.Length : default(short));

        length += directoryInfo
                  .GetDirectories()
                  .Sum(nextdir => nextdir.Exists ? nextdir.GetSize() : default(short));
        // ----------------------------------------------------------------------------------------------------
        return length;
    }
}