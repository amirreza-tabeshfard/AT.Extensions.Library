using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToSignedBytesExtensions
{
    private static Byte[] ExecuteCompression(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null");

            Byte[] xmlBytes;

            if (input is XmlDocument doc)
            {
                using var memoryStream = new MemoryStream();
                doc.Save(memoryStream);
                xmlBytes = memoryStream.ToArray();
            }
            else if (input is String str)
                xmlBytes = Encoding.UTF8.GetBytes(str);
            else if (input is MemoryStream ms)
                xmlBytes = ms.ToArray();
            else if (input is StreamReader sr)
                xmlBytes = Encoding.UTF8.GetBytes(sr.ReadToEnd());
            else if (input is XmlElement element)
                xmlBytes = Encoding.UTF8.GetBytes(element.OuterXml);
            else if (input is XmlNode node)
                xmlBytes = Encoding.UTF8.GetBytes(node.OuterXml);
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("XML file not found", fileInfo.FullName);
             
                xmlBytes = File.ReadAllBytes(fileInfo.FullName);
            }
            else if (input is TextReader textReader)
                xmlBytes = Encoding.UTF8.GetBytes(textReader.ReadToEnd());
            else if (input is Byte[] bytes)
                xmlBytes = bytes;
            else if (input is StringBuilder sb)
                xmlBytes = Encoding.UTF8.GetBytes(sb.ToString());
            else if (input is XmlNodeList nodeList)
            {
                var sbTemp = new StringBuilder();
                foreach (XmlNode n in nodeList)
                    sbTemp.Append(n.OuterXml);
                xmlBytes = Encoding.UTF8.GetBytes(sbTemp.ToString());
            }
            else if (input is XmlWriter writer)
            {
                writer.Flush();
                throw new NotSupportedException("XmlWriter input requires manual retrieval of content");
            }
            else if (input is XmlAttribute attr)
                xmlBytes = Encoding.UTF8.GetBytes(attr.OuterXml);
            else if (input is Uri uri)
            {
                using var webClient = new System.Net.WebClient();
                xmlBytes = webClient.DownloadData(uri);
            }
            else
                throw new NotSupportedException($"Input type {input.GetType().FullName} is not supported for XML compression");

            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(xmlBytes);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed: Input reference cannot be null", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: XML file '{ex.FileName}' not found", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("XmlWriter"))
        {
            throw new InvalidOperationException("Compression failed: XmlWriter input requires manual retrieval of content", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("not supported"))
        {
            throw new InvalidOperationException($"Compression failed: {ex.Message}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: I/O error occurred in source '{ex.Source}'", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException($"Compression failed: Unauthorized access - {ex.Message}", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: Object '{ex.ObjectName}' has been disposed", ex);
        }
        catch (XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"Compression failed: XML parsing error at line {ex.LineNumber}, position {ex.LinePosition}", ex);
        }
        catch (DecoderFallbackException ex) when (ex.BytesUnknown is not null && ex.BytesUnknown.Length > 0)
        {
            throw new InvalidOperationException("Compression failed: UTF8 decoding error in XML content", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression failed: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToSignedBytesExtensions
{
    public static Byte[] CompressXmlToSignedBytes(this XmlDocument xmlDoc)
    {
        return ExecuteCompression(xmlDoc);
    }

    public static Byte[] CompressXmlToSignedBytes(this String xmlString)
    {
        return ExecuteCompression(xmlString);
    }

    public static Byte[] CompressXmlToSignedBytes(this MemoryStream xmlStream)
    {
        return ExecuteCompression(xmlStream);
    }

    public static Byte[] CompressXmlToSignedBytes(this StreamReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Byte[] CompressXmlToSignedBytes(this XmlElement xmlElement)
    {
        return ExecuteCompression(xmlElement);
    }

    public static Byte[] CompressXmlToSignedBytes(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Byte[] CompressXmlToSignedBytes(this FileInfo xmlFile)
    {
        return ExecuteCompression(xmlFile);
    }

    public static Byte[] CompressXmlToSignedBytes(this TextReader textReader)
    {
        return ExecuteCompression(textReader);
    }

    public static Byte[] CompressXmlToSignedBytes(this Byte[] xmlBytes)
    {
        return ExecuteCompression(xmlBytes);
    }

    public static Byte[] CompressXmlToSignedBytes(this StringBuilder xmlBuilder)
    {
        return ExecuteCompression(xmlBuilder);
    }

    public static Byte[] CompressXmlToSignedBytes(this XmlNodeList xmlNodeList)
    {
        return ExecuteCompression(xmlNodeList);
    }

    public static Byte[] CompressXmlToSignedBytes(this XmlWriter xmlWriter)
    {
        return ExecuteCompression(xmlWriter);
    }

    public static Byte[] CompressXmlToSignedBytes(this Object xmlObject)
    {
        return ExecuteCompression(xmlObject);
    }

    public static Byte[] CompressXmlToSignedBytes(this XmlAttribute xmlAttribute)
    {
        return ExecuteCompression(xmlAttribute);
    }

    public static Byte[] CompressXmlToSignedBytes(this Uri xmlUri)
    {
        return ExecuteCompression(xmlUri);
    }
}