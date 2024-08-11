using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.FileInfos.Copy;
public static class CopyToFileExtensions : Object
{
    public static void CopyToFile(this System.Text.StringBuilder contents, String path)
    {
        ArgumentNullException.ThrowIfNull(contents);
        // ----------------------------------------------------------------------------------------------------
        File.WriteAllText(path, contents.ToString());
    }

    public static void CopyToFile(this String contents, String path)
    {
        if (contents.IsNullOrEmpty() || contents.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(contents));
        // ----------------------------------------------------------------------------------------------------
        File.WriteAllText(path, contents);
    }
}