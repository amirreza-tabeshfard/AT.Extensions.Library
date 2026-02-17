namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class CombineExtenstions
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