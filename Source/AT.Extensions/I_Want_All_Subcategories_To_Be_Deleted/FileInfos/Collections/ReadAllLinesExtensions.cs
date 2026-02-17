using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Boundary;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.FileInfos.Collections;
public static class ReadAllLinesExtensions
{
    public static String[] ReadAllLines(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath);
    }

    public static String[] ReadAllLines(this String filePath, System.Text.Encoding encoding)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        else if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        // ----------------------------------------------------------------------------------------------------
        return File.ReadAllLines(filePath, encoding);
    }

    public static IEnumerable<String?>? ReadAllLines(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        using StreamReader reader = file.ThrowIfNotFound().OpenText();
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }
}