using AT.Extensions.FileInfos.Comparison;
using MimeDetective;

namespace AT.Extensions.FileInfos.Extraction;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly HashSet<char> _NotValidPathChars;

    #endregion

    #region Constructor

    static Extensions()
    {
        _NotValidPathChars = new(Path.GetInvalidFileNameChars());
    }

    #endregion

    /// <summary>
    /// This Show How to Get File Extension Form ByteArray
    /// </summary>
    /// <example>
    /// <code>
    /// FileStream fileStream = new FileStream(path: "[FULL PATH]", mode: FileMode.Open);
    /// byte[] bfile = new byte[fileStream.Length];
    /// fileStream.Read(bfile, 0, Convert.ToInt32(bfile.Length));
    /// string fileExtension = GetFileExtensionFormByteArray(bfile);
    /// </code>
    /// </example>
    /// <param name="byteArray">Byte Array From File</param>
    /// <returns>Extension name</returns>
    public static string? GetFileExtensionFormByteArray(this byte[] byteArray)
    {
        if (byteArray == default)
            throw new ArgumentNullException($"byteArray is '{default(byte[])}'");
        // ----------------------------------------------------------------------------------------------------
        Stream stream = new MemoryStream(byteArray);
        byte[] content = ContentReader.Default.ReadFromStream(stream);
        // ----------------------------------------------------------------------------------------------------
        return new ContentInspectorBuilder()
        {
            Definitions = MimeDetective.Definitions.Default.All()
        }
               .Build()
               .Inspect(content)
               .ByFileExtension()
               .FirstOrDefault()
               ?.Extension
               .ToString();
    }

    public static string GetFileNameWithoutExtension(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException($"file is '{default(FileInfo)}'");
        // ----------------------------------------------------------------------------------------------------
        return Path.GetFileNameWithoutExtension(file.Name);
    }

    public static long GetFileSize(this String filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentNullException($"filePath is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        long result = default;
        FileInfo? fileInfo = default;
        // ---------------------------------------------------
        if (filePath.IsFileExists() is default(bool))
            result = -1;
        else
            fileInfo = new FileInfo(filePath);

        if (fileInfo is not null)
            result = fileInfo.Length;
        // ---------------------------------------------------
        return result;
    }

    public static string? GetFreeName(this String name, IEnumerable<string> names, string pattern = "{0} ({1})")
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException($"name is '{default}'");
        else if (string.IsNullOrEmpty(pattern))
            throw new ArgumentNullException($"pattern is '{default}'");
        else if (names == default)
            throw new ArgumentNullException($"names is '{default}'");
        else if (!names.Any())
            throw new ArgumentNullException($"names is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        string? result = default;
        HashSet<string> hashSetNames = new(names);
        for (int i = 1; hashSetNames.Contains(name); i++)
            result = string.Format(pattern, name, i);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static string GetFullFileNameWithNewExtension(this FileInfo file, string extension)
    {
        if (file == default)
            throw new ArgumentNullException($"file is '{default}'");
        else if (string.IsNullOrEmpty(extension))
            throw new ArgumentNullException($"extension is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return Path.ChangeExtension(file.FullName, extension);
    }

    public static string GetFullFileNameWithoutExtension(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException($"file is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return Path.Combine(file.Directory!.FullName, file.GetFileNameWithoutExtension());
    }

    public static string GetValidFileName(this String Name, char replacementSymbol = '_')
    {
        if (string.IsNullOrWhiteSpace(Name))
            return "new_file";

        string newName = Name.Trim(' ');

        return _NotValidPathChars.Aggregate(newName, (current, c) => current.Replace(c, replacementSymbol));
    }
}