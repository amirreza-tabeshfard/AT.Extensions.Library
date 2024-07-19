using AT.Extensions.FileInfos.Comparison;
using AT.Extensions.Strings.Comparison;
using MimeDetective;

namespace AT.Extensions.FileInfos.Extraction;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly HashSet<Char> _NotValidPathChars;

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
    /// String fileExtension = GetFileExtensionFormByteArray(bfile);
    /// </code>
    /// </example>
    /// <param name="byteArray">Byte Array From File</param>
    /// <returns>Extension name</returns>
    public static String? GetFileExtensionFormByteArray(this byte[] byteArray)
    {
        if (byteArray == default)
            throw new ArgumentNullException(nameof(byteArray));
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

    public static String GetFileNameWithoutExtension(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return Path.GetFileNameWithoutExtension(file.Name);
    }

    public static Int64 GetFileSize(this String filePath)
    {
        if (filePath.IsNullOrEmpty() || filePath.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(filePath));
        // ----------------------------------------------------------------------------------------------------
        Int64 result = default;
        FileInfo? fileInfo = default;
        // ---------------------------------------------------
        if (filePath.IsFileExists() is default(Boolean))
            result = -1;
        else
            fileInfo = new FileInfo(filePath);

        if (fileInfo is not null)
            result = fileInfo.Length;
        // ---------------------------------------------------
        return result;
    }

    public static String? GetFreeName(this String name, IEnumerable<String> names, String pattern = "{0} ({1})")
    {
        if (name.IsNullOrEmpty() || name.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(name));
        else if (pattern.IsNullOrEmpty() || pattern.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(pattern));
        else if (names == default)
            throw new ArgumentNullException(nameof(names));
        else if (!names.Any())
            throw new ArgumentNullException(nameof(names));
        // ----------------------------------------------------------------------------------------------------
        String? result = default;
        HashSet<String> hashSetNames = new(names);
        for (Int32 i = 1; hashSetNames.Contains(name); i++)
            result = String.Format(pattern, name, i);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String GetFullFileNameWithNewExtension(this FileInfo file, String extension)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        else if (extension.IsNullOrEmpty() || extension.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(extension));
        // ----------------------------------------------------------------------------------------------------
        return Path.ChangeExtension(file.FullName, extension);
    }

    public static String GetFullFileNameWithoutExtension(this FileInfo file)
    {
        if (file == default)
            throw new ArgumentNullException(nameof(file));
        // ----------------------------------------------------------------------------------------------------
        return Path.Combine(file.Directory!.FullName, file.GetFileNameWithoutExtension());
    }

    public static String GetValidFileName(this String Name, Char replacementSymbol = '_')
    {
        if (String.IsNullOrWhiteSpace(Name))
            return "new_file";

        String newName = Name.Trim(' ');

        return _NotValidPathChars.Aggregate(newName, (current, c) => current.Replace(c, replacementSymbol));
    }
}