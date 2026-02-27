using System.IO.Compression;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToGzipStreamExtensions
{
    private static Stream ExecuteCompression(Object xmlInput)
    {
        try
        {
            if (xmlInput is null)
                throw new ArgumentNullException("Input cannot be null");

            String xmlContent = null;

            if (xmlInput is XmlDocument xmlDoc)
                xmlContent = xmlDoc.OuterXml;
            else if (xmlInput is XDocument xDoc)
                xmlContent = xDoc.ToString(SaveOptions.DisableFormatting);
            else if (xmlInput is String xmlString)
                xmlContent = xmlString;
            else if (xmlInput is XmlElement xmlElement)
                xmlContent = xmlElement.OuterXml;
            else if (xmlInput is XmlNode xmlNode)
                xmlContent = xmlNode.OuterXml;
            else if (xmlInput is XElement xElement)
                xmlContent = xElement.ToString(SaveOptions.DisableFormatting);
            else if (xmlInput is MemoryStream memStream)
            {
                memStream.Position = 0;
                using var reader = new StreamReader(memStream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (xmlInput is FileInfo fileInfo)
            {
                if (fileInfo.Exists == false)
                    throw new FileNotFoundException("File not found", fileInfo.FullName);

                xmlContent = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            }
            else if (xmlInput is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (xmlInput is StreamReader streamReader)
                xmlContent = streamReader.ReadToEnd();
            else if (xmlInput is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.Load(xmlReader);
                xmlContent = doc.OuterXml;
            }
            else if (xmlInput is Uri uri)
            {
                using var webClient = new WebClient();
                xmlContent = webClient.DownloadString(uri);
            }
            else if (xmlInput is Byte[] byteArray)
                xmlContent = Encoding.UTF8.GetString(byteArray);
            else if (xmlInput is Stream stream)
            {
                stream.Position = 0;
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else
                throw new InvalidCastException("Unsupported XML input type: " + xmlInput.GetType().FullName);

            var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal, true))
            {
                var bytes = Encoding.UTF8.GetBytes(xmlContent);
                gzipStream.Write(bytes, 0, bytes.Length);
            }

            outputStream.Position = 0;
            return outputStream;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("Input cannot be null"))
        {
            throw new InvalidOperationException("Compression failed: Input XML Object is null", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException("Compression failed: XML file not found at specified path", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("Unsupported XML input type"))
        {
            throw new InvalidOperationException("Compression failed: Unsupported XML input type provided", ex);
        }
        catch (XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException("Compression failed: Invalid XML format detected", ex);
        }
        catch (IOException ex) when (ex.HResult != 0)
        {
            throw new InvalidOperationException("Compression failed: I/O error occurred during XML reading or writing", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException("Compression failed: Stream or reader has already been disposed", ex);
        }
        catch (WebException ex) when (ex.Status.Equals(System.Net.WebExceptionStatus.ConnectFailure))
        {
            throw new InvalidOperationException("Compression failed: Unable to download XML content from URI", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed: Unexpected error occurred", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToGzipStreamExtensions
{
    public static Stream CompressXmlToGzipStream(this XmlDocument xmlDoc)
    {
        return ExecuteCompression(xmlDoc);
    }

    public static Stream CompressXmlToGzipStream(this XDocument xDoc)
    {
        return ExecuteCompression(xDoc);
    }

    public static Stream CompressXmlToGzipStream(this String xmlString)
    {
        return ExecuteCompression(xmlString);
    }

    public static Stream CompressXmlToGzipStream(this XmlElement xmlElement)
    {
        return ExecuteCompression(xmlElement);
    }

    public static Stream CompressXmlToGzipStream(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Stream CompressXmlToGzipStream(this XElement xElement)
    {
        return ExecuteCompression(xElement);
    }

    public static Stream CompressXmlToGzipStream(this MemoryStream xmlStream)
    {
        return ExecuteCompression(xmlStream);
    }

    public static Stream CompressXmlToGzipStream(this FileInfo xmlFile)
    {
        return ExecuteCompression(xmlFile);
    }

    public static Stream CompressXmlToGzipStream(this TextReader textReader)
    {
        return ExecuteCompression(textReader);
    }

    public static Stream CompressXmlToGzipStream(this StreamReader streamReader)
    {
        return ExecuteCompression(streamReader);
    }

    public static Stream CompressXmlToGzipStream(this XmlReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Stream CompressXmlToGzipStream(this Uri xmlUri)
    {
        return ExecuteCompression(xmlUri);
    }

    public static Stream CompressXmlToGzipStream(this Byte[] xmlBytes)
    {
        return ExecuteCompression(xmlBytes);
    }

    public static Stream CompressXmlToGzipStream(this Object genericXmlObject)
    {
        return ExecuteCompression(genericXmlObject);
    }

    public static Stream CompressXmlToGzipStream(this Stream xmlStream)
    {
        return ExecuteCompression(xmlStream);
    }
}