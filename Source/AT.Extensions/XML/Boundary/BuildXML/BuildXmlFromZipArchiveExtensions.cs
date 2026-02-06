using System.IO.Compression;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromZipArchiveExtensions
{
    private static XDocument ProcessZipArchive(Object input)
    {
        try
        {
            var xmlDoc = new XDocument(new XElement("Root"));

            if (input is ZipArchive archiveInput)
            {
                foreach (var entry in archiveInput.Entries)
                    if (!String.IsNullOrWhiteSpace(entry.Name))
                    {
                        var element = new XElement("File", entry.Name);
                        xmlDoc.Root.Add(element);
                    }
            }
            else if (input is FileInfo fileInfoInput)
            {
                using var zipFromFile = ZipFile.OpenRead(fileInfoInput.FullName);
                xmlDoc = ProcessZipArchive(zipFromFile);
            }
            else if (input is String pathInput)
            {
                if (!File.Exists(pathInput))
                    throw new FileNotFoundException("The provided ZIP file path does not exist.", pathInput);
                
                using var zipFromPath = ZipFile.OpenRead(pathInput);
                xmlDoc = ProcessZipArchive(zipFromPath);
            }
            else if (input is Stream streamInput)
            {
                using var zipFromStream = new ZipArchive(streamInput, ZipArchiveMode.Read, true);
                xmlDoc = ProcessZipArchive(zipFromStream);
            }
            else if (input is Byte[] bytesInput)
            {
                using var memStream = new MemoryStream(bytesInput);
                xmlDoc = ProcessZipArchive(memStream);
            }
            else if (input is ZipArchiveEntry entryInput)
            {
                using var entryStream = entryInput.Open();
                xmlDoc = ProcessZipArchive(entryStream);
            }
            else if (input is DirectoryInfo dirInput)
            {
                foreach (var file in dirInput.GetFiles("*.zip"))
                {
                    using var archiveFromDir = ZipFile.OpenRead(file.FullName);
                    var childDoc = ProcessZipArchive(archiveFromDir);
                    xmlDoc.Root.Add(childDoc.Root.Elements());
                }
            }
            else if (input is Uri uriInput)
            {
                using var client = new System.Net.Http.HttpClient();
                var bytes = client.GetByteArrayAsync(uriInput).GetAwaiter().GetResult();
                xmlDoc = ProcessZipArchive(bytes);
            }
            else if (input is TextReader readerInput)
            {
                var content = readerInput.ReadToEnd();
                xmlDoc = XDocument.Parse(content);
            }
            else if (input is XElement elementInput)
            {
                xmlDoc = new XDocument(new XElement(elementInput));
            }
            else if (input is XmlDocument xmlDocInput)
            {
                using var nodeReader = new XmlNodeReader(xmlDocInput);
                xmlDoc = XDocument.Load(nodeReader);
            }
            else if (input is XmlReader xmlReaderInput)
            {
                xmlDoc = XDocument.Load(xmlReaderInput);
            }
            else if (input is StreamReader streamReaderInput)
            {
                var content = streamReaderInput.ReadToEnd();
                xmlDoc = XDocument.Parse(content);
            }
            else if (input != null)
            {
                var serialized = System.Text.Json.JsonSerializer.Serialize(input);
                xmlDoc.Root.Add(new XElement("SerializedObject", serialized));
            }
            else
                throw new ArgumentNullException(nameof(input), "Input cannot be null for BuildXmlFromZipArchive.");

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input parameter is null. Cannot build XML from a null ZIP archive.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.EndsWith(".zip"))
        {
            throw new InvalidOperationException($"ZIP file not found at path '{ex.FileName}'.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("The ZIP archive is invalid or corrupted.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the ZIP archive.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("The operation is not supported on this ZIP archive.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the ZIP file or directory is denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to parse XML content from the ZIP archive.", ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http"))
        {
            throw new InvalidOperationException("Failed to download the ZIP file from the specified URI.", ex);
        }
        catch (JsonException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Json"))
        {
            throw new InvalidOperationException("Failed to serialize the input Object to XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from ZIP archive.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromZipArchiveExtensions
{
    public static XDocument BuildXmlFromZipArchive(this ZipArchive zipArchive)
    {
        return ProcessZipArchive(zipArchive);
    }

    public static XDocument BuildXmlFromZipArchive(this FileInfo zipFile)
    {
        return ProcessZipArchive(zipFile);
    }

    public static XDocument BuildXmlFromZipArchive(this String zipFilePath)
    {
        return ProcessZipArchive(zipFilePath);
    }

    public static XDocument BuildXmlFromZipArchive(this Stream zipStream)
    {
        return ProcessZipArchive(zipStream);
    }

    public static XDocument BuildXmlFromZipArchive(this MemoryStream zipMemoryStream)
    {
        return ProcessZipArchive(zipMemoryStream);
    }

    public static XDocument BuildXmlFromZipArchive(this DirectoryInfo directory)
    {
        return ProcessZipArchive(directory);
    }

    public static XDocument BuildXmlFromZipArchive(this Uri zipUri)
    {
        return ProcessZipArchive(zipUri);
    }

    public static XDocument BuildXmlFromZipArchive(this Byte[] zipBytes)
    {
        return ProcessZipArchive(zipBytes);
    }

    public static XDocument BuildXmlFromZipArchive(this TextReader textReader)
    {
        return ProcessZipArchive(textReader);
    }

    public static XDocument BuildXmlFromZipArchive(this XElement xmlElement)
    {
        return ProcessZipArchive(xmlElement);
    }

    public static XDocument BuildXmlFromZipArchive(this XmlDocument xmlDocument)
    {
        return ProcessZipArchive(xmlDocument);
    }

    public static XDocument BuildXmlFromZipArchive(this XmlReader xmlReader)
    {
        return ProcessZipArchive(xmlReader);
    }

    public static XDocument BuildXmlFromZipArchive(this Object someObject)
    {
        return ProcessZipArchive(someObject);
    }

    public static XDocument BuildXmlFromZipArchive(this StreamReader streamReader)
    {
        return ProcessZipArchive(streamReader);
    }

    public static XDocument BuildXmlFromZipArchive(this ZipArchiveEntry zipEntry)
    {
        return ProcessZipArchive(zipEntry);
    }
}