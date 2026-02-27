using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithChecksumExtensions
{
    private static String CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            String xmlContent = null;

            if (input is XmlDocument doc)
                xmlContent = doc.OuterXml;
            else if (input is XmlElement element)
                xmlContent = element.OuterXml;
            else if (input is XmlNode node)
                xmlContent = node.OuterXml;
            else if (input is String str)
                xmlContent = str;
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (input is FileInfo fileInfo)
            {
                using var reader = fileInfo.OpenText();
                xmlContent = reader.ReadToEnd();
            }
            else if (input is Uri uri)
            {
                using var client = new System.Net.WebClient();
                xmlContent = client.DownloadString(uri);
            }
            else if (input is Byte[] bytes)
                xmlContent = Encoding.UTF8.GetString(bytes);
            else
                throw new ArgumentException("Unsupported input type: " + input.GetType().FullName);

            if (String.IsNullOrWhiteSpace(xmlContent))
                throw new InvalidDataException("XML content is empty or whitespace");

            Byte[] compressedBytes;
            using (var outputStream = new MemoryStream())
            {
                using (var gzip = new GZipStream(outputStream, CompressionLevel.Optimal, true))
                using (var writer = new StreamWriter(gzip, Encoding.UTF8))
                {
                    writer.Write(xmlContent);
                }
                compressedBytes = outputStream.ToArray();
            }

            using var sha256 = SHA256.Create();
            var checksum = sha256.ComputeHash(compressedBytes);
            var checksumString = BitConverter.ToString(checksum).Replace("-", "");

            var base64Compressed = Convert.ToBase64String(compressedBytes);
            return $"{base64Compressed}:{checksumString}";
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed: Input type is unsupported.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed: Input cannot be null.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message.Contains("empty") || ex.Message.Contains("whitespace"))
        {
            throw new InvalidOperationException("Compression failed: XML content is empty or whitespace.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed: IO error occurred while reading input or writing compressed output.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Encoding"))
        {
            throw new InvalidOperationException("Compression failed: The provided encoding is not supported.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Compression failed: Stream was already disposed before operation.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access"))
        {
            throw new InvalidOperationException("Compression failed: Access denied to file or resource.", ex);
        }
        catch (System.Net.WebException ex) when (ex.Status.Equals(System.Net.WebExceptionStatus.ConnectFailure))
        {
            throw new InvalidOperationException("Compression failed: Failed to download XML content from URI.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("GZipStream"))
        {
            throw new InvalidOperationException("Compression failed: Error occurred during GZip compression.", ex);
        }
        catch (CryptographicException ex) when (ex.Message.Contains("SHA256"))
        {
            throw new InvalidOperationException("Compression failed: Error occurred while computing SHA256 checksum.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed: Unexpected error occurred.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlWithChecksumExtensions
{
    public static String CompressXmlWithChecksum(this XmlDocument xmlDocument)
    {
        return CompressInternal(xmlDocument);
    }

    public static String CompressXmlWithChecksum(this XmlElement xmlElement)
    {
        return CompressInternal(xmlElement);
    }

    public static String CompressXmlWithChecksum(this XmlNode xmlNode)
    {
        return CompressInternal(xmlNode);
    }

    public static String CompressXmlWithChecksum(this String xmlString)
    {
        return CompressInternal(xmlString);
    }

    public static String CompressXmlWithChecksum(this Stream xmlStream)
    {
        return CompressInternal(xmlStream);
    }

    public static String CompressXmlWithChecksum(this TextReader xmlReader)
    {
        return CompressInternal(xmlReader);
    }

    public static String CompressXmlWithChecksum(this XmlReader xmlReader)
    {
        return CompressInternal(xmlReader);
    }

    public static String CompressXmlWithChecksum(this MemoryStream memoryStream)
    {
        return CompressInternal(memoryStream);
    }

    public static String CompressXmlWithChecksum(this StringReader stringReader)
    {
        return CompressInternal(stringReader);
    }

    public static String CompressXmlWithChecksum(this XmlNodeReader xmlNodeReader)
    {
        return CompressInternal(xmlNodeReader);
    }

    public static String CompressXmlWithChecksum(this FileInfo xmlFile)
    {
        return CompressInternal(xmlFile);
    }

    public static String CompressXmlWithChecksum(this Uri xmlUri)
    {
        return CompressInternal(xmlUri);
    }

    public static String CompressXmlWithChecksum(this Byte[] xmlBytes)
    {
        return CompressInternal(xmlBytes);
    }

    public static String CompressXmlWithChecksum(this TextWriter textWriter)
    {
        return CompressInternal(textWriter);
    }

    public static String CompressXmlWithChecksum(this Object xmlObject)
    {
        return CompressInternal(xmlObject);
    }
}