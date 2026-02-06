using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromEpubFileExtenstions
{
    private static XDocument Synchronize(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            ZipArchive? archive = null;

            if (input is String path)
            {
                if (String.IsNullOrWhiteSpace(path))
                    throw new ArgumentException("EPUB path is empty or whitespace.", nameof(input));

                if (!File.Exists(path))
                    throw new FileNotFoundException("EPUB file was not found on disk.", path);

                archive = ZipFile.OpenRead(path);
            }
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("EPUB FileInfo does not exist.", fi.FullName);

                archive = ZipFile.OpenRead(fi.FullName);
            }
            else if (input is MemoryStream ms)
                archive = new ZipArchive(new MemoryStream(ms.ToArray()), ZipArchiveMode.Read, false);
            else if (input is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Provided stream is not readable.");

                using var temp = new MemoryStream();
                stream.CopyTo(temp);
                archive = new ZipArchive(new MemoryStream(temp.ToArray()), ZipArchiveMode.Read, false);
            }
            else if (input is Byte[] bytes)
                archive = new ZipArchive(new MemoryStream(bytes), ZipArchiveMode.Read, false);
            else if (input is IReadOnlyList<Byte> roBytes)
            {
                var buffer = new Byte[roBytes.Count];
                for (int i = 0; i < roBytes.Count; i++)
                    buffer[i] = roBytes[i];

                archive = new ZipArchive(new MemoryStream(buffer), ZipArchiveMode.Read, false);
            }
            else if (input is ReadOnlyCollection<Byte> roc)
                archive = new ZipArchive(new MemoryStream(new List<Byte>(roc).ToArray()), ZipArchiveMode.Read, false);
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file:// URIs are supported for EPUB.");

                archive = ZipFile.OpenRead(uri.LocalPath);
            }
            else if (input is ZipArchive za)
                archive = za;
            else if (input is XElement xe)
                return new XDocument(new XElement("EpubXml", xe));
            else if (input is XDocument xd)
                return xd;
            else if (input is TextReader tr)
                return XDocument.Parse(tr.ReadToEnd());
            else if (input is BinaryReader br)
            {
                var data = br.ReadBytes((int)br.BaseStream.Length);
                archive = new ZipArchive(new MemoryStream(data), ZipArchiveMode.Read, false);
            }
            else if (input is DirectoryInfo di)
            {
                if (!di.Exists)
                    throw new DirectoryNotFoundException("Provided directory does not exist.");

                var root = new XElement("EpubDirectory",
                    new XAttribute("path", di.FullName));

                foreach (var file in di.GetFiles("*", SearchOption.AllDirectories))
                    root.Add(new XElement("File", new XAttribute("name", file.Name), new XAttribute("length", file.Length)));

                return new XDocument(root);
            }
            else
                throw new NotSupportedException($"Unsupported input type: {input.GetType().FullName}");

            if (archive is null)
                throw new InvalidOperationException("Unable to resolve EPUB archive from provided input.");

            var epubRoot = new XElement("Epub",
                new XAttribute("entries", archive.Entries.Count));

            foreach (var entry in archive.Entries)
                epubRoot.Add(new XElement("Entry", new XAttribute("fullName", entry.FullName), new XAttribute("compressedSize", entry.CompressedLength), new XAttribute("size", entry.Length)));

            return new XDocument(epubRoot);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Input String is empty or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Input reference cannot be null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("does not exist"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Provided directory does not exist.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"BuildXmlFromEpubFile failed: EPUB file was not found on disk: {ex.FileName}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("stream is not readable"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Provided stream is not readable.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Unable to resolve EPUB archive"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Unable to resolve EPUB archive from provided input.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Only file:// URIs are supported"))
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed: Only file:// URIs are supported for EPUB input.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported input type"))
        {
            throw new InvalidOperationException($"BuildXmlFromEpubFile failed: Unsupported input type encountered. Type: {ex.GetType().FullName}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("BuildXmlFromEpubFile failed while processing the provided input. See inner exception for details.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromEpubFileExtenstions
{

    public static XDocument BuildXmlFromEpubFile(this String epubPath)
    {
        return Synchronize(epubPath);
    }

    public static XDocument BuildXmlFromEpubFile(this FileInfo fileInfo)
    {
        return Synchronize(fileInfo);
    }

    public static XDocument BuildXmlFromEpubFile(this Stream stream)
    {
        return Synchronize(stream);
    }

    public static XDocument BuildXmlFromEpubFile(this MemoryStream memoryStream)
    {
        return Synchronize(memoryStream);
    }

    public static XDocument BuildXmlFromEpubFile(this Byte[] buffer)
    {
        return Synchronize(buffer);
    }

    public static XDocument BuildXmlFromEpubFile(this Uri uri)
    {
        return Synchronize(uri);
    }

    public static XDocument BuildXmlFromEpubFile(this ZipArchive archive)
    {
        return Synchronize(archive);
    }

    public static XDocument BuildXmlFromEpubFile(this XElement element)
    {
        return Synchronize(element);
    }

    public static XDocument BuildXmlFromEpubFile(this XDocument document)
    {
        return Synchronize(document);
    }

    public static XDocument BuildXmlFromEpubFile(this TextReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromEpubFile(this BinaryReader reader)
    {
        return Synchronize(reader);
    }

    public static XDocument BuildXmlFromEpubFile(this DirectoryInfo directory)
    {
        return Synchronize(directory);
    }

    public static XDocument BuildXmlFromEpubFile(this IReadOnlyList<Byte> readOnlyBytes)
    {
        return Synchronize(readOnlyBytes);
    }

    public static XDocument BuildXmlFromEpubFile(this ReadOnlyCollection<Byte> readOnlyCollection)
    {
        return Synchronize(readOnlyCollection);
    }

    public static XDocument BuildXmlFromEpubFile(this Object unknown)
    {
        return Synchronize(unknown);
    }
}