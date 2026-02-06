using AT.Extensions.FileInfos.Comparison;
using MimeDetective;

namespace AT.Extensions.FileInfos.Extraction;
public static class GetExtensions
{
    #region Field(s)

    private static readonly HashSet<Char> _NotValidPathChars;

    #endregion

    #region Constructor

    static GetExtensions()
    {
        _NotValidPathChars = new(Path.GetInvalidFileNameChars());
    }

    #endregion

    public static String? GetFileExtensionFormByteArray(this byte[] byteArray)
    {
        ArgumentNullException.ThrowIfNull(byteArray);
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
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return Path.GetFileNameWithoutExtension(file.Name);
    }

    public static Int64 GetFileSize(this String filePath)
    {
        ArgumentException.ThrowIfNullOrEmpty(filePath);
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
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(names);
        ArgumentException.ThrowIfNullOrEmpty(pattern);
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
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrEmpty(extension);
        // ----------------------------------------------------------------------------------------------------
        return Path.ChangeExtension(file.FullName, extension);
    }

    public static String GetFullFileNameWithoutExtension(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        return Path.Combine(file.Directory!.FullName, file.GetFileNameWithoutExtension());
    }

    public static String GetValidFileName(this String name, Char replacementSymbol = '_')
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        // ----------------------------------------------------------------------------------------------------
        String newName = name.Trim(' ');
        // ----------------------------------------------------------------------------------------------------
        return _NotValidPathChars.Aggregate(newName, (current, c) => current.Replace(c, replacementSymbol));
    }
}