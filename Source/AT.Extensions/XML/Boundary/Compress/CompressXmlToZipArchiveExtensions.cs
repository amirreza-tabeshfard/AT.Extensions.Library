using System.IO.Compression;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToZipArchiveExtensions
{
    private static Byte[] CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input Object cannot be null.");

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                if (input is XmlDocument xmlDoc)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    writer.Write(xmlDoc.OuterXml);
                }
                else if (input is String xmlString)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    writer.Write(xmlString);
                }
                else if (input is FileInfo fileInfo)
                {
                    if (!fileInfo.Exists)
                        throw new FileNotFoundException($"File not found: {fileInfo.FullName}");

                    var entry = archive.CreateEntry(fileInfo.Name);
                    using var entryStream = entry.Open();
                    using var fileStream = fileInfo.OpenRead();
                    fileStream.CopyTo(entryStream);
                }
                else if (input is Stream stream)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    stream.CopyTo(entryStream);
                }
                else if (input is XmlReader xmlReader)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    while (xmlReader.Read())
                        writer.Write(xmlReader.ReadOuterXml());
                }
                else if (input is TextReader textReader)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    writer.Write(textReader.ReadToEnd());
                }
                else if (input is Uri uri)
                {
                    if (uri.Scheme != Uri.UriSchemeFile)
                        throw new NotSupportedException("Only file URIs are supported.");

                    var filePath = uri.LocalPath;
                    var fileInfoFromUri = new FileInfo(filePath);
                    if (!fileInfoFromUri.Exists)
                        throw new FileNotFoundException($"File not found: {fileInfoFromUri.FullName}");

                    var entry = archive.CreateEntry(fileInfoFromUri.Name);
                    using var entryStream = entry.Open();
                    using var fileStream = fileInfoFromUri.OpenRead();
                    fileStream.CopyTo(entryStream);
                }
                else if (input is MemoryStream memStream)
                {
                    var entry = archive.CreateEntry("document.xml");
                    using var entryStream = entry.Open();
                    memStream.CopyTo(entryStream);
                }
                else if (input is String[] xmlStringArray)
                {
                    var counter = 0;
                    foreach (var str in xmlStringArray)
                    {
                        var entry = archive.CreateEntry($"document_{counter}.xml");
                        using (var entryStream = entry.Open())
                        using (var writer = new StreamWriter(entryStream))
                        {
                            writer.Write(str);
                        }

                        counter++;
                    }
                }
                else if (input is XmlNode xmlNode)
                {
                    var entry = archive.CreateEntry("node.xml");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    writer.Write(xmlNode.OuterXml);
                }
                else if (input is XmlNodeList xmlNodeList)
                {
                    var counter = 0;
                    foreach (XmlNode node in xmlNodeList)
                    {
                        var entry = archive.CreateEntry($"node_{counter}.xml");
                        using (var entryStream = entry.Open())
                        using (var writer = new StreamWriter(entryStream))
                        {
                            writer.Write(node.OuterXml);
                        }

                        counter++;
                    }
                }
                else if (input is DirectoryInfo dirInfo)
                {
                    if (!dirInfo.Exists)
                        throw new DirectoryNotFoundException($"Directory not found: {dirInfo.FullName}");

                    var xmlFiles = dirInfo.GetFiles("*.xml");
                    foreach (var xmlFile in xmlFiles)
                    {
                        var entry = archive.CreateEntry(xmlFile.Name);
                        using var entryStream = entry.Open();
                        using var fileStream = xmlFile.OpenRead();
                        fileStream.CopyTo(entryStream);
                    }
                }
                else if (input is Object obj)
                {
                    var entry = archive.CreateEntry("Object.txt");
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    writer.Write(obj.ToString() ?? String.Empty);
                }
                else if (input is TextReader[] textReaders)
                {
                    var counter = 0;
                    foreach (var reader in textReaders)
                    {
                        var entry = archive.CreateEntry($"textReader_{counter}.xml");
                        using (var entryStream = entry.Open())
                        using (var writer = new StreamWriter(entryStream))
                        {
                            writer.Write(reader.ReadToEnd());
                        }

                        counter++;
                    }
                }
                else
                    throw new NotSupportedException($"Unsupported input type: {input.GetType().FullName}");
            }

            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed: the input Object is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: the specified directory was not found. Details: {ex.Message}", ex);
        }
        catch (FileNotFoundException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: the specified file was not found. Details: {ex.Message}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: unsupported operation encountered. Details: {ex.Message}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"Compression failed: IO error occurred while reading or writing streams. Source: {ex.Source}", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: access to file or directory is denied. Details: {ex.Message}", ex);
        }
        catch (XmlException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: invalid XML encountered. Details: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression failed: an unexpected error occurred. Details: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 14 )
/// </summary>
public static partial class CompressXmlToZipArchiveExtensions
{
    public static Byte[] CompressXmlToZipArchive(this XmlDocument xmlDocument)
    {
        return CompressInternal(xmlDocument);
    }

    public static Byte[] CompressXmlToZipArchive(this String xmlString)
    {
        return CompressInternal(xmlString);
    }

    public static Byte[] CompressXmlToZipArchive(this FileInfo xmlFile)
    {
        return CompressInternal(xmlFile);
    }

    public static Byte[] CompressXmlToZipArchive(this Stream xmlStream)
    {
        return CompressInternal(xmlStream);
    }

    public static Byte[] CompressXmlToZipArchive(this XmlReader xmlReader)
    {
        return CompressInternal(xmlReader);
    }

    public static Byte[] CompressXmlToZipArchive(this TextReader textReader)
    {
        return CompressInternal(textReader);
    }

    public static Byte[] CompressXmlToZipArchive(this Uri xmlUri)
    {
        return CompressInternal(xmlUri);
    }

    public static Byte[] CompressXmlToZipArchive(this MemoryStream memoryStream)
    {
        return CompressInternal(memoryStream);
    }

    public static Byte[] CompressXmlToZipArchive(this String[] xmlStrings)
    {
        return CompressInternal(xmlStrings);
    }

    public static Byte[] CompressXmlToZipArchive(this XmlNode xmlNode)
    {
        return CompressInternal(xmlNode);
    }

    public static Byte[] CompressXmlToZipArchive(this XmlNodeList xmlNodeList)
    {
        return CompressInternal(xmlNodeList);
    }

    public static Byte[] CompressXmlToZipArchive(this DirectoryInfo directoryInfo)
    {
        return CompressInternal(directoryInfo);
    }

    public static Byte[] CompressXmlToZipArchive(this Object xmlObject)
    {
        return CompressInternal(xmlObject);
    }

    public static Byte[] CompressXmlToZipArchive(this TextReader[] textReaders)
    {
        return CompressInternal(textReaders);
    }
}