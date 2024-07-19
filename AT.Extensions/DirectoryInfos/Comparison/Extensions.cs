using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DirectoryInfos.Comparison;
public static class Extensions
{
    #region Method(s): Private

    private static T NotNull<T>(this T? obj, String? Message = default) 
        where T : class
    {
        return obj ?? throw new InvalidOperationException(Message ?? "Empty object reference");
    }

    private static DirectoryInfo ThrowIfNotFound(this DirectoryInfo Dir, String? Message = default)
    {
        DirectoryInfo dir = Dir.NotNull("Missing directory link");
        return !dir.Exists 
               ? throw new DirectoryNotFoundException(Message ?? $"Directory '{dir.FullName}' not found") 
               : dir;
    }

    #endregion

    public static Boolean IsDirectoryExists(this String path)
    {
        if (path.IsNullOrEmpty() || path.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        Boolean result = default;    
        // ----------------------------------------------------------------------------------------------------
        if (Directory.Exists(path))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean IsDirectoryHaveLockFile(this DirectoryInfo target)
    {
        if (target == default)
            throw new ArgumentNullException(nameof(target));
        // ----------------------------------------------------------------------------------------------------
        return target
               .ThrowIfNotFound()
               .IsLockFileInDirectory()
               || target
               .GetDirectories(searchOption: SearchOption.AllDirectories, searchPattern: ".")
               .Any(d => d.IsLockFileInDirectory());
    }

    public static Boolean IsLockFileInDirectory(this DirectoryInfo directory)
    {
        if (directory == default)
            throw new ArgumentNullException(nameof(directory));
        // ----------------------------------------------------------------------------------------------------
        return directory
               .GetFiles()
               .Any(file => file.IsReadOnly);
    }
}