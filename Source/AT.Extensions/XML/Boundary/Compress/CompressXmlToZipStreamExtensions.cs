using System.IO.Compression;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToZipStreamExtensions
{
    private static Byte[] CompressXmlPrivate(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            String xmlContent = null;

            if (input is XmlDocument xmlDoc)
            {
                using var stringWriter = new StringWriter();
                xmlDoc.Save(stringWriter);
                xmlContent = stringWriter.ToString();
            }
            else if (input is String xmlString)
                xmlContent = xmlString;
            else if (input is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                using var reader = new StreamReader(memoryStream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("XML file not found.", fileInfo.FullName);

                xmlContent = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            }
            else if (input is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (input is StreamReader streamReader)
                xmlContent = streamReader.ReadToEnd();
            else if (input is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.Load(xmlReader);
                using var stringWriter = new StringWriter();
                doc.Save(stringWriter);
                xmlContent = stringWriter.ToString();
            }
            else if (input is Byte[] bytes)
                xmlContent = Encoding.UTF8.GetString(bytes);
            else if (input is StringBuilder stringBuilder)
                xmlContent = stringBuilder.ToString();
            else if (input is Uri uri)
            {
                using var client = new WebClient();
                xmlContent = client.DownloadString(uri);
            }
            else if (input is DirectoryInfo dirInfo)
                throw new ArgumentException("Cannot compress a directory directly. Provide XML files.", nameof(input));
            else if (input is XmlNode xmlNode)
                xmlContent = xmlNode.OuterXml;
            else if (input is XmlNodeList xmlNodeList)
            {
                var sb = new StringBuilder();
                foreach (XmlNode node in xmlNodeList)
                    sb.Append(node.OuterXml);
                xmlContent = sb.ToString();
            }
            else
                throw new ArgumentException("Unsupported input type for XML compression.", nameof(input));

            if (String.IsNullOrWhiteSpace(xmlContent))
                throw new InvalidOperationException("XML content is empty and cannot be compressed.");

            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var zipEntry = zipArchive.CreateEntry("compressed.xml", CompressionLevel.Optimal);
                    using var entryStream = zipEntry.Open();
                    using var writer = new StreamWriter(entryStream, Encoding.UTF8);
                    writer.Write(xmlContent);
                }

                return memoryStream.ToArray();
            }
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed due to invalid input argument: '{ex.ParamName}'.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed because input is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("cannot compress a directory"))
        {
            throw new InvalidOperationException("Compression failed: Directory cannot be compressed directly. Provide XML files instead.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: XML file not found at '{ex.FileName}'.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to an I/O error while reading or writing the XML data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("XML content is empty"))
        {
            throw new InvalidOperationException("Compression failed because XML content is empty.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Unsupported input type"))
        {
            throw new InvalidOperationException("Compression failed due to unsupported input type provided.", ex);
        }
        catch (WebException ex) when (ex.Status.Equals(System.Net.WebExceptionStatus.ProtocolError))
        {
            throw new InvalidOperationException("Compression failed: Unable to download XML from the provided URI.", ex);
        }
        catch (SecurityException ex) when (ex.Message.Contains("access"))
        {
            throw new InvalidOperationException("Compression failed due to insufficient security permissions.", ex);
        }
        catch (XmlException ex) when (ex.Message.Contains("Data at the root level"))
        {
            throw new InvalidOperationException("Compression failed: Invalid XML format detected.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to compress XML to ZIP stream: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToZipStreamExtensions
{
    public static Byte[] CompressXmlToZipStream(this XmlDocument xmlDocument)
    {
        return CompressXmlPrivate(xmlDocument);
    }

    public static Byte[] CompressXmlToZipStream(this String xmlContent)
    {
        return CompressXmlPrivate(xmlContent);
    }

    public static Byte[] CompressXmlToZipStream(this MemoryStream xmlStream)
    {
        return CompressXmlPrivate(xmlStream);
    }

    public static Byte[] CompressXmlToZipStream(this FileInfo xmlFile)
    {
        return CompressXmlPrivate(xmlFile);
    }

    public static Byte[] CompressXmlToZipStream(this TextReader textReader)
    {
        return CompressXmlPrivate(textReader);
    }

    public static Byte[] CompressXmlToZipStream(this StreamReader streamReader)
    {
        return CompressXmlPrivate(streamReader);
    }

    public static Byte[] CompressXmlToZipStream(this XmlReader xmlReader)
    {
        return CompressXmlPrivate(xmlReader);
    }

    public static Byte[] CompressXmlToZipStream(this Byte[] xmlBytes)
    {
        return CompressXmlPrivate(xmlBytes);
    }

    public static Byte[] CompressXmlToZipStream(this StringBuilder xmlBuilder)
    {
        return CompressXmlPrivate(xmlBuilder);
    }

    public static Byte[] CompressXmlToZipStream(this Uri xmlUri)
    {
        return CompressXmlPrivate(xmlUri);
    }

    public static Byte[] CompressXmlToZipStream(this DirectoryInfo directoryInfo)
    {
        return CompressXmlPrivate(directoryInfo);
    }

    public static Byte[] CompressXmlToZipStream(this Object xmlObject)
    {
        return CompressXmlPrivate(xmlObject);
    }

    public static Byte[] CompressXmlToZipStream(this XmlNode xmlNode)
    {
        return CompressXmlPrivate(xmlNode);
    }

    public static Byte[] CompressXmlToZipStream(this XmlNodeList xmlNodeList)
    {
        return CompressXmlPrivate(xmlNodeList);
    }

    public static Byte[] CompressXmlToZipStream(this XmlElement xmlElement)
    {
        return CompressXmlPrivate(xmlElement);
    }
}