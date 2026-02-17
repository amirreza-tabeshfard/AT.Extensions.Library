using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlBase64Extensions
{
    private static String CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            var xmlString = String.Empty;

            if (input is String str)
                xmlString = str;
            else if (input is XmlDocument xmlDoc)
                xmlString = xmlDoc.OuterXml;
            else if (input is XmlNode xmlNode)
                xmlString = xmlNode.OuterXml;
            else if (input is XmlElement xmlElement)
                xmlString = xmlElement.OuterXml;
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else if (input is Char[] charArray)
                xmlString = new String(charArray);
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is TextReader textReader)
                xmlString = textReader.ReadToEnd();
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("File does not exist", fileInfo.FullName);

                xmlString = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new ArgumentException("Uri must be absolute", nameof(uri));

                using var client = new System.Net.Http.HttpClient();
                var task = client.GetStringAsync(uri);
                task.Wait();
                xmlString = task.Result;
            }
            else
                throw new InvalidOperationException($"Unsupported input type: {input.GetType().FullName}");

            var bytes = Encoding.UTF8.GetBytes(xmlString);

            using var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal, true))
            {
                gzipStream.Write(bytes, 0, bytes.Length);
            }

            return Convert.ToBase64String(outputStream.ToArray());
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed due to invalid argument: input cannot be null or invalid", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException("Compression failed because the specified file was not found: " + ex.FileName, ex);
        }
        catch (HttpRequestException ex) when (ex.Source is not null && ex.Source.Equals("System.Net.Http.HttpClient"))
        {
            throw new InvalidOperationException("Compression failed while fetching XML from Uri: network request failed", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("AT.Extensions.XML.Boundary.Compress.XmlCompressionExtensions"))
        {
            throw new InvalidOperationException("Compression failed due to unsupported input type", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to IO error while reading input", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Compression failed because the input stream has been disposed", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to insufficient permissions to access the file or stream", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Compression failed due to encoding error while reading input", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed due to an unexpected error: " + (ex.Source is not null ? ex.Source : "Unknown source"), ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlBase64Extensions
{
    public static String CompressXmlBase64(this String xmlContent)
    {
        return CompressInternal(xmlContent);
    }

    public static String CompressXmlBase64(this XmlDocument xmlDoc)
    {
        return CompressInternal(xmlDoc);
    }

    public static String CompressXmlBase64(this XmlElement xmlElement)
    {
        return CompressInternal(xmlElement);
    }

    public static String CompressXmlBase64(this XmlNode xmlNode)
    {
        return CompressInternal(xmlNode);
    }

    public static String CompressXmlBase64(this Object xmlObject)
    {
        return CompressInternal(xmlObject);
    }

    public static String CompressXmlBase64(this Stream xmlStream)
    {
        return CompressInternal(xmlStream);
    }

    public static String CompressXmlBase64(this TextReader textReader)
    {
        return CompressInternal(textReader);
    }

    public static String CompressXmlBase64(this StringBuilder stringBuilder)
    {
        return CompressInternal(stringBuilder);
    }

    public static String CompressXmlBase64(this Char[] charArray)
    {
        return CompressInternal(charArray);
    }

    public static String CompressXmlBase64(this FileInfo fileInfo)
    {
        return CompressInternal(fileInfo);
    }

    public static String CompressXmlBase64(this Uri uri)
    {
        return CompressInternal(uri);
    }

    public static String CompressXmlBase64(this MemoryStream memoryStream)
    {
        return CompressInternal(memoryStream);
    }

    public static String CompressXmlBase64(this XmlNodeList xmlNodeList)
    {
        return CompressInternal(xmlNodeList);
    }

    public static String CompressXmlBase64(this XmlAttribute xmlAttribute)
    {
        return CompressInternal(xmlAttribute);
    }

    public static String CompressXmlBase64(this XmlNamedNodeMap xmlNamedNodeMap)
    {
        return CompressInternal(xmlNamedNodeMap);
    }
}