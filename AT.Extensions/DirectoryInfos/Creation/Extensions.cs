using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DirectoryInfos.Creation;
public static class Extensions : Object
{
    public static void CreateDirectory(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        DirectoryInfo directoryInfo = new(path);
        // ----------------------------------------------------------------------------------------------------
        if (directoryInfo.Parent is not null)
            directoryInfo.Parent.CreateDirectory();

        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }

    public static void CreateDirectory(this DirectoryInfo directoryInfo)
    {
        if (directoryInfo == default)
            throw new ArgumentNullException(nameof(directoryInfo));
        // ----------------------------------------------------------------------------------------------------
        if (directoryInfo.Parent is not null)
            directoryInfo.Parent.CreateDirectory();

        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }
}