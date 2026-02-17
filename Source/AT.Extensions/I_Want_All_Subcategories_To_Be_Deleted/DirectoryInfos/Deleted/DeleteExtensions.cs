using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DirectoryInfos.Creation;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DirectoryInfos.Deleted;
public static class DeleteExtensions
{
    public static void DeleteDirectory(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);
        // ----------------------------------------------------------------------------------------------------
        directoryInfo.Parent?.DeleteDirectory();

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
        directoryInfo.Parent?.CreateDirectory();

        if (directoryInfo.Exists)
            directoryInfo.Delete();
    }
}