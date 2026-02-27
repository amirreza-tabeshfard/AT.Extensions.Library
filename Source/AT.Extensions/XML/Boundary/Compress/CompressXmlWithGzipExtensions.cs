using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithGzipExtensions
{
    private static Byte[] CompressXmlInternal(Object xmlInput)
    {
        try
        {
            String xmlString = null;

            if (xmlInput is String s)
            {
                if (String.IsNullOrWhiteSpace(s))
                    throw new ArgumentException("Input String is null or empty");
                xmlString = s;
            }
            else if (xmlInput is XmlDocument doc)
            {
                if (doc.DocumentElement == null)
                    throw new ArgumentException("XmlDocument has no root element");
                xmlString = doc.OuterXml;
            }
            else if (xmlInput is XmlElement element)
                xmlString = element.OuterXml;
            else if (xmlInput is XmlNode node)
                xmlString = node.OuterXml;
            else if (xmlInput is XmlNodeList nodeList)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < nodeList.Count; i++)
                    if (nodeList[i] != null)
                        builder.Append(nodeList[i].OuterXml);
                xmlString = builder.ToString();
            }
            else if (xmlInput is StringBuilder sb)
            {
                if (sb.Length == 0)
                    throw new ArgumentException("StringBuilder is empty");
                xmlString = sb.ToString();
            }
            else if (xmlInput is TextReader reader)
            {
                xmlString = reader.ReadToEnd();
                if (String.IsNullOrWhiteSpace(xmlString))
                    throw new ArgumentException("TextReader contains no content");
            }
            else if (xmlInput is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException("File does not exist", file.FullName);
                xmlString = File.ReadAllText(file.FullName);
            }
            else if (xmlInput is Stream stream)
            {
                if (!stream.CanRead)
                    throw new ArgumentException("Stream cannot be read");
                stream.Position = 0;
                using var readerStream = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlString = readerStream.ReadToEnd();
            }
            else if (xmlInput is XmlReader xmlReader)
            {
                var docReader = new XmlDocument();
                docReader.Load(xmlReader);
                xmlString = docReader.OuterXml;
            }
            else if (xmlInput is MemoryStream ms)
            {
                if (ms.Length == 0)
                    throw new ArgumentException("MemoryStream is empty");
                ms.Position = 0;
                using var readerMs = new StreamReader(ms, Encoding.UTF8, true, 1024, true);
                xmlString = readerMs.ReadToEnd();
            }
            else if (xmlInput is XmlAttribute attr)
                xmlString = attr.OuterXml;
            else if (xmlInput is XmlNamedNodeMap namedMap)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < namedMap.Count; i++)
                    if (namedMap.Item(i) != null)
                        builder.Append(namedMap.Item(i).OuterXml);
                xmlString = builder.ToString();
            }
            else if (xmlInput is XmlCDataSection cdata)
                xmlString = cdata.OuterXml;
            else if (xmlInput is XmlWhitespace ws)
                xmlString = ws.OuterXml;
            else
                throw new ArgumentException("Unsupported type for XML compression: " + xmlInput.GetType().FullName);

            var bytes = Encoding.UTF8.GetBytes(xmlString);
            using var memoryStream = new MemoryStream();
            using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal, true))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }
            memoryStream.Position = 0;
            return memoryStream.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlInput"))
        {
            throw new InvalidOperationException("Compression failed due to invalid argument: input is null, empty, or invalid type", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlInput"))
        {
            throw new InvalidOperationException("Compression failed due to null reference input", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(ex.FileName))
        {
            throw new InvalidOperationException("Compression failed because the specified file was not found", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to an IO error while reading input", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("Compression failed because the stream does not support reading or writing", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Compression failed because the stream has already been disposed", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Compression failed due to invalid XML structure or operation on XML Object", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Message.Equals(ex.Message))
        {
            throw new InvalidOperationException("Compression failed due to an encoding failure in converting XML to bytes", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed due to an unexpected error: " + ex.Message, ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlWithGzipExtensions
{
    public static Byte[] CompressXmlWithGzip(this String xmlContent)
    {
        return CompressXmlInternal(xmlContent);
    }

    public static Byte[] CompressXmlWithGzip(this XmlDocument xmlDocument)
    {
        return CompressXmlInternal(xmlDocument);
    }

    public static Byte[] CompressXmlWithGzip(this XmlElement xmlElement)
    {
        return CompressXmlInternal(xmlElement);
    }

    public static Byte[] CompressXmlWithGzip(this XmlNode xmlNode)
    {
        return CompressXmlInternal(xmlNode);
    }

    public static Byte[] CompressXmlWithGzip(this XmlNodeList xmlNodeList)
    {
        return CompressXmlInternal(xmlNodeList);
    }

    public static Byte[] CompressXmlWithGzip(this StringBuilder xmlBuilder)
    {
        return CompressXmlInternal(xmlBuilder);
    }

    public static Byte[] CompressXmlWithGzip(this TextReader textReader)
    {
        return CompressXmlInternal(textReader);
    }

    public static Byte[] CompressXmlWithGzip(this FileInfo xmlFile)
    {
        return CompressXmlInternal(xmlFile);
    }

    public static Byte[] CompressXmlWithGzip(this Stream xmlStream)
    {
        return CompressXmlInternal(xmlStream);
    }

    public static Byte[] CompressXmlWithGzip(this XmlReader xmlReader)
    {
        return CompressXmlInternal(xmlReader);
    }

    public static Byte[] CompressXmlWithGzip(this MemoryStream memoryStream)
    {
        return CompressXmlInternal(memoryStream);
    }

    public static Byte[] CompressXmlWithGzip(this XmlAttribute xmlAttribute)
    {
        return CompressXmlInternal(xmlAttribute);
    }

    public static Byte[] CompressXmlWithGzip(this XmlNamedNodeMap namedNodeMap)
    {
        return CompressXmlInternal(namedNodeMap);
    }

    public static Byte[] CompressXmlWithGzip(this XmlCDataSection cdataSection)
    {
        return CompressXmlInternal(cdataSection);
    }

    public static Byte[] CompressXmlWithGzip(this XmlWhitespace xmlWhitespace)
    {
        return CompressXmlInternal(xmlWhitespace);
    }
}