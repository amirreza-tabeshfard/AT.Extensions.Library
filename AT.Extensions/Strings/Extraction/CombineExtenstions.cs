namespace AT.Extensions.Strings.Extraction;
public static class CombineExtenstions : Object
{
    public static String CombineToPath(this String path, String root)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrEmpty(root);
        // ----------------------------------------------------------------------------------------------------
        if (Path.IsPathRooted(path)) 
            return path;
        // ----------------------------------------------------------------------------------------------------
        return Path.Combine(root, path);
    }
}