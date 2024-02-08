namespace AT.Extensions.DirectoryInfos.Comparison;
public static class Extensions
{
    #region Method(s): Private

    private static T NotNull<T>(this T? obj, string? Message = default) 
        where T : class
    {
        return obj ?? throw new InvalidOperationException(Message ?? "Empty object reference");
    }

    private static DirectoryInfo ThrowIfNotFound(this DirectoryInfo Dir, string? Message = default)
    {
        DirectoryInfo dir = Dir.NotNull("Missing directory link");
        return !dir.Exists 
               ? throw new DirectoryNotFoundException(Message ?? $"Directory '{dir.FullName}' not found") 
               : dir;
    }

    #endregion

    public static bool IsDirectoryExists(this String path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentNullException(nameof(path));
        // ----------------------------------------------------------------------------------------------------
        bool result = default;    
        // ----------------------------------------------------------------------------------------------------
        if (Directory.Exists(path))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsDirectoryHaveLockFile(this DirectoryInfo target)
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

    public static bool IsLockFileInDirectory(this DirectoryInfo directory)
    {
        if (directory == default)
            throw new ArgumentNullException(nameof(directory));
        // ----------------------------------------------------------------------------------------------------
        return directory
               .GetFiles()
               .Any(file => file.IsReadOnly);
    }
}