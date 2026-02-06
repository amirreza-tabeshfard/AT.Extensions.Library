using System.IO.Compression;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromFileExtensions
{
    private static XDocument Synchronize(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            if (source is XDocument doc)
                return new XDocument(doc);

            if (source is String path)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("XML file path does not exist.", path);

                using var fs = File.OpenRead(path);
                return LoadFromStream(fs);
            }

            if (source is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException("FileInfo does not reference an existing file.", file.FullName);

                using var fs = file.OpenRead();
                return LoadFromStream(fs);
            }

            if (source is DirectoryInfo dir)
            {
                if (!dir.Exists)
                    throw new DirectoryNotFoundException("Directory does not exist.");

                var xml = dir.GetFiles("*.xml").FirstOrDefault() ?? throw new FileNotFoundException("No XML files found in directory.");

                using var fs = xml.OpenRead();
                return LoadFromStream(fs);
            }

            if (source is IEnumerable<String> paths)
            {
                var first = paths.FirstOrDefault() ?? throw new InvalidOperationException("Path collection is empty.");

                using var fs = File.OpenRead(first);
                return LoadFromStream(fs);
            }

            if (source is Uri uri)
            {
                if (!uri.IsFile)
                    throw new NotSupportedException("Only file based Uri is supported.");

                using var fs = File.OpenRead(uri.LocalPath);
                return LoadFromStream(fs);
            }

            if (source is Byte[] buffer)
            {
                ValidateBoundary(buffer.LongLength);
                using var ms = new MemoryStream(buffer);
                return XDocument.Load(ms);
            }

            if (source is BinaryReader br)
            {
                var bytes = br.ReadBytes(int.MaxValue);
                ValidateBoundary(bytes.LongLength);
                using var ms = new MemoryStream(bytes);
                return XDocument.Load(ms);
            }

            if (source is ZipArchiveEntry entry)
            {
                using var s = entry.Open();
                return LoadFromStream(s);
            }

            if (source is HttpContent content)
            {
                using var s = content.ReadAsStream();
                return LoadFromStream(s);
            }

            if (source is StreamReader sr)
                return XDocument.Load(sr);

            if (source is TextReader tr)
                return XDocument.Load(tr);

            if (source is Stream stream)
                return LoadFromStream(stream);

            throw new NotSupportedException($"Type '{source.GetType().FullName}' is not supported.");
        }
        catch (ArgumentException ex)when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("The provided file path argument is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The source argument is malformed or unsupported.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("Source cannot be null for XML synchronization.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Target directory was not found during XML discovery.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Referenced XML file does not exist on disk.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("XmlFileExtensionsNet8"))
        {
            throw new InvalidOperationException("XML boundary validation failed due to invalid data size.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("XML parsing failed due to invalid document structure.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Stream operation failed during XML loading.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided source type is not supported for BuildXmlFromFile.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An attempt was made to access a disposed stream.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML file or directory was denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML content is malformed or contains invalid characters.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("BuildXmlFromFile failed due to an unexpected runtime error.", ex);
        }
    }

    private static XDocument LoadFromStream(Stream stream)
    {
        if (!stream.CanRead)
            throw new InvalidOperationException("Provided stream is not readable.");

        using var ms = new MemoryStream();
        stream.CopyTo(ms);

        ValidateBoundary(ms.Length);

        ms.Position = 0;
        return XDocument.Load(ms);
    }

    private static void ValidateBoundary(long length)
    {
        if (length <= 0)
            throw new InvalidDataException("XML source is empty.");

        const long Max = 50 * 1024 * 1024;

        if (length > Max)
            throw new InvalidDataException("XML source exceeds 50 MB boundary.");
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromFileExtensions
{
    public static XDocument BuildXmlFromFile(this String path)
    {
        return Synchronize(path);
    }

    public static XDocument BuildXmlFromFile(this FileInfo file)
    {
        return Synchronize(file);
    }

    public static XDocument BuildXmlFromFile(this DirectoryInfo directory)
    {
        return Synchronize(directory);
    }

    public static XDocument BuildXmlFromFile(this IEnumerable<String> paths)
    {
        return Synchronize(paths);
    }

    public static XDocument BuildXmlFromFile(this Stream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromFile(this FileStream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromFile(this MemoryStream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromFile(this Byte[] buffer)
    {
        return Synchronize(buffer);
    }

    public static XDocument BuildXmlFromFile(this Uri uri)
    {
        return Synchronize(uri);
    }

    public static XDocument BuildXmlFromFile(this StreamReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromFile(this TextReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromFile(this BinaryReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromFile(this ZipArchiveEntry entry)
    {
        return Synchronize(entry);
    }

    public static XDocument BuildXmlFromFile(this HttpContent content)
    {
        return Synchronize(content);
    }

    public static XDocument BuildXmlFromFile(this XDocument document)
    {
        return Synchronize(document);
    }
}