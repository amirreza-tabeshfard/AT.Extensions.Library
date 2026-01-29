using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DirectoryInfos.Creation;
public static class CreateExtensions
{
    public static void CreateDirectory(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        DirectoryInfo directoryInfo = new(path);
        // ----------------------------------------------------------------------------------------------------
        directoryInfo.Parent?.CreateDirectory();

        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }

    public static void CreateDirectory(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        directoryInfo.Parent?.CreateDirectory();

        if (!directoryInfo.Exists)
            directoryInfo.Create();
    }
}