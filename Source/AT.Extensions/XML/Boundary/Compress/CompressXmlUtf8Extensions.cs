using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlUtf8Extensions
{
    private static dynamic CompressXmlPrivate(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            String xmlString = null;

            if (input is String s)
                xmlString = s;
            else if (input is XDocument xDoc)
                xmlString = xDoc.ToString();
            else if (input is XElement xElem)
                xmlString = xElem.ToString();
            else if (input is XmlDocument xmlDoc)
            {
                using var sw = new StringWriter();
                xmlDoc.Save(sw);
                xmlString = sw.ToString();
            }
            else if (input is XmlElement xmlEl)
                xmlString = xmlEl.OuterXml;
            else if (input is XmlNode xmlNode)
                xmlString = xmlNode.OuterXml;
            else if (input is TextReader reader)
                xmlString = reader.ReadToEnd();
            else if (input is Stream stream)
            {
                using var sr = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlString = sr.ReadToEnd();
            }
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else if (input is Uri uri)
            {
                using var webClient = new System.Net.WebClient();
                xmlString = webClient.DownloadString(uri);
            }
            else if (input is FileInfo fileInfo)
                xmlString = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            else if (input is DirectoryInfo dirInfo)
            {
                var files = dirInfo.GetFiles("*.xml");
                var combined = new StringBuilder();
                foreach (var file in files)
                    combined.AppendLine(File.ReadAllText(file.FullName, Encoding.UTF8));
                xmlString = combined.ToString();
            }
            else if (input is Object obj)
                xmlString = obj.ToString();
            else if (input is XElement[] xElems)
            {
                var combined = new StringBuilder();
                foreach (var elem in xElems)
                    combined.AppendLine(elem.ToString());
                xmlString = combined.ToString();
            }
            else if (input is XDocument[] xDocs)
            {
                var combined = new StringBuilder();
                foreach (var doc in xDocs)
                    combined.AppendLine(doc.ToString());
                xmlString = combined.ToString();
            }
            else
                throw new NotSupportedException($"Type {input.GetType().FullName} is not supported for XML compression.");

            var xmlBytes = Encoding.UTF8.GetBytes(xmlString);

            using var ms = new MemoryStream();
            using (var gzip = new GZipStream(ms, CompressionLevel.Optimal, true))
            {
                gzip.Write(xmlBytes, 0, xmlBytes.Length);
            }

            ms.Position = 0;

            if (input is String || input is XmlDocument || input is Uri || input is Object)
                return Convert.ToBase64String(ms.ToArray());
            else if (input is XDocument || input is StringBuilder || input is FileInfo || input is XElement[])
                return ms.ToArray();
            else
                return ms;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"XML compression failed due to null input. Parameter: {ex.ParamName}. Source: {ex.Source}", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"XML compression failed because the specified file was not found. File: {ex.FileName}. Source: {ex.Source}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException($"XML compression failed due to an I/O error. Source: {ex.Source}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("not supported"))
        {
            throw new InvalidOperationException($"XML compression failed due to unsupported type. Source: {ex.Source}", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException($"XML compression failed because the stream or Object was already disposed. Object: {ex.ObjectName}. Source: {ex.Source}", ex);
        }
        catch (XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML compression failed due to invalid XML format at line {ex.LineNumber}, position {ex.LinePosition}. Source: {ex.Source}", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message is not null && ex.Message.Contains("access"))
        {
            throw new InvalidOperationException($"XML compression failed due to insufficient access permissions. Source: {ex.Source}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Contains("GZipStream"))
        {
            throw new InvalidOperationException($"XML compression failed during GZip compression operation. Source: {ex.Source}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"XML compression failed due to an unexpected error. Source: {ex.Source}. Message: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlUtf8Extensions
{
    public static String CompressXmlUtf8(this String xmlContent)
    {
        return CompressXmlPrivate(xmlContent);
    }

    public static Byte[] CompressXmlUtf8(this XDocument xmlDocument)
    {
        return CompressXmlPrivate(xmlDocument);
    }

    public static MemoryStream CompressXmlUtf8(this XElement xmlElement)
    {
        return CompressXmlPrivate(xmlElement);
    }

    public static String CompressXmlUtf8(this XmlDocument xmlDoc)
    {
        return CompressXmlPrivate(xmlDoc);
    }

    public static Byte[] CompressXmlUtf8(this XmlElement xmlElement)
    {
        return CompressXmlPrivate(xmlElement);
    }

    public static MemoryStream CompressXmlUtf8(this TextReader reader)
    {
        return CompressXmlPrivate(reader);
    }

    public static String CompressXmlUtf8(this Stream xmlStream)
    {
        return CompressXmlPrivate(xmlStream);
    }

    public static Byte[] CompressXmlUtf8(this StringBuilder xmlBuilder)
    {
        return CompressXmlPrivate(xmlBuilder);
    }

    public static MemoryStream CompressXmlUtf8(this XmlNode xmlNode)
    {
        return CompressXmlPrivate(xmlNode);
    }

    public static String CompressXmlUtf8(this Uri xmlUri)
    {
        return CompressXmlPrivate(xmlUri);
    }

    public static Byte[] CompressXmlUtf8(this FileInfo xmlFile)
    {
        return CompressXmlPrivate(xmlFile);
    }

    public static MemoryStream CompressXmlUtf8(this DirectoryInfo xmlDirectory)
    {
        return CompressXmlPrivate(xmlDirectory);
    }

    public static String CompressXmlUtf8(this Object xmlObject)
    {
        return CompressXmlPrivate(xmlObject);
    }

    public static Byte[] CompressXmlUtf8(this XElement[] xmlElements)
    {
        return CompressXmlPrivate(xmlElements);
    }

    public static MemoryStream CompressXmlUtf8(this XDocument[] xmlDocuments)
    {
        return CompressXmlPrivate(xmlDocuments);
    }
}