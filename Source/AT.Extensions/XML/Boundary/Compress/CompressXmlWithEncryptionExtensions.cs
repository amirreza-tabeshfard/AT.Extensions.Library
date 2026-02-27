using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithEncryptionExtensions
{
    private static Byte[] ExecuteCompression(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            String xmlContent = null;

            if (input is XDocument xDoc)
                xmlContent = xDoc.ToString();
            else if (input is XmlDocument xmlDoc)
            {
                using var stringWriter = new StringWriter();
                xmlDoc.Save(stringWriter);
                xmlContent = stringWriter.ToString();
            }
            else if (input is String str)
                xmlContent = str;
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, leaveOpen: true);
                xmlContent = reader.ReadToEnd();
                stream.Position = 0;
            }
            else if (input is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (input is StringBuilder sb)
                xmlContent = sb.ToString();
            else if (input is MemoryStream memStream)
            {
                memStream.Position = 0;
                using var reader = new StreamReader(memStream, leaveOpen: true);
                xmlContent = reader.ReadToEnd();
                memStream.Position = 0;
            }
            else if (input is XmlReader xmlReader)
            {
                var doc = new XDocument();
                doc = XDocument.Load(xmlReader);
                xmlContent = doc.ToString();
            }
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("XML file does not exist", fileInfo.FullName);
                xmlContent = File.ReadAllText(fileInfo.FullName);
            }
            else if (input is char[] charArray)
                xmlContent = new String(charArray);
            else if (input is Byte[] byteArray)
                xmlContent = Encoding.UTF8.GetString(byteArray);
            else if (input is Uri uri)
            {
                using var webClient = new System.Net.WebClient();
                xmlContent = webClient.DownloadString(uri);
            }
            else if (input is XElement xElem)
                xmlContent = xElem.ToString();
            else
                throw new ArgumentException("Unsupported input type for XML compression");

            var plainBytes = Encoding.UTF8.GetBytes(xmlContent);
            using var compressedStream = new MemoryStream();
            using (var gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, leaveOpen: true))
            {
                gzip.Write(plainBytes, 0, plainBytes.Length);
            }

            var compressedBytes = compressedStream.ToArray();
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();
            using var encryptStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(encryptStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(compressedBytes, 0, compressedBytes.Length);
            }

            return encryptStream.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Invalid argument for XML compression: parameter '{ex.ParamName}' is not supported");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input cannot be null for XML compression");
        }
        catch (CryptographicException ex) when (ex.Source is not null && ex.Source.Equals("System.Security.Cryptography"))
        {
            throw new InvalidOperationException("AES encryption failed during XML compression");
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"XML file does not exist: '{ex.FileName}'");
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("IO operation failed during XML compression");
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("GZip compression is not supported for the provided stream");
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Length > 0)
        {
            throw new InvalidOperationException($"Attempted to access a disposed Object: '{ex.ObjectName}' during compression");
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML parsing failed during compression");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression and encryption failed: {ex}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlWithEncryptionExtensions
{
    public static Byte[] CompressXmlWithEncryption(this XDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlWithEncryption(this XmlDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlWithEncryption(this String xmlString)
    {
        return ExecuteCompression(xmlString);
    }

    public static Byte[] CompressXmlWithEncryption(this Stream xmlStream)
    {
        return ExecuteCompression(xmlStream);
    }

    public static Byte[] CompressXmlWithEncryption(this TextReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Byte[] CompressXmlWithEncryption(this StringBuilder xmlBuilder)
    {
        return ExecuteCompression(xmlBuilder);
    }

    public static Byte[] CompressXmlWithEncryption(this MemoryStream memoryStream)
    {
        return ExecuteCompression(memoryStream);
    }

    public static Byte[] CompressXmlWithEncryption(this XmlReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Byte[] CompressXmlWithEncryption(this FileInfo xmlFile)
    {
        return ExecuteCompression(xmlFile);
    }

    public static Byte[] CompressXmlWithEncryption(this char[] xmlChars)
    {
        return ExecuteCompression(xmlChars);
    }

    public static Byte[] CompressXmlWithEncryption(this Byte[] xmlBytes)
    {
        return ExecuteCompression(xmlBytes);
    }

    public static Byte[] CompressXmlWithEncryption(this Object xmlObject)
    {
        return ExecuteCompression(xmlObject);
    }

    public static Byte[] CompressXmlWithEncryption(this Uri xmlUri)
    {
        return ExecuteCompression(xmlUri);
    }

    public static Byte[] CompressXmlWithEncryption(this TextWriter xmlWriter)
    {
        return ExecuteCompression(xmlWriter);
    }

    public static Byte[] CompressXmlWithEncryption(this XElement xmlElement)
    {
        return ExecuteCompression(xmlElement);
    }
}