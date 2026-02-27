using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToEncryptedBytesExtensions
{
    private static Byte[] CompressInternal(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input value cannot be null.");

            var xml = String.Empty;

            if (input is String s)
                xml = s;
            else if (input is StringBuilder sb)
                xml = sb.ToString();
            else if (input is XmlDocument xd)
                xml = xd.OuterXml;
            else if (input is XmlElement xe)
                xml = xe.OuterXml;
            else if (input is XmlNode xn)
                xml = xn.OuterXml;
            else if (input is XDocument xdoc)
                xml = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xel)
                xml = xel.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xml = doc.OuterXml;
            }
            else if (input is MemoryStream ms)
                xml = Encoding.UTF8.GetString(ms.ToArray());
            else if (input is Stream st)
            {
                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xml = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                xml = tr.ReadToEnd();
            else if (input is FileInfo fi)
                xml = File.ReadAllText(fi.FullName, Encoding.UTF8);
            else if (input is Uri uri)
                xml = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
            else if (input is Byte[] bytes)
                xml = Encoding.UTF8.GetString(bytes);
            else
                throw new NotSupportedException("The provided input type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xml))
                throw new InvalidOperationException("Resolved XML content is empty or whitespace.");

            var rawBytes = Encoding.UTF8.GetBytes(xml);

            Byte[] compressed;
            using (var output = new MemoryStream())
            {
                using (var gzip = new GZipStream(output, CompressionLevel.Optimal, true))
                    gzip.Write(rawBytes, 0, rawBytes.Length);

                compressed = output.ToArray();
            }

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var encryptedStream = new MemoryStream();

            encryptedStream.Write(aes.IV, 0, aes.IV.Length);

            using (var crypto = new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write))
                crypto.Write(compressed, 0, compressed.Length);

            return encryptedStream.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The input argument is invalid or improperly formatted.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A required input parameter was not provided.", ex);
        }
        catch (CryptographicException ex) when (ex.Source is not null && ex.Source.Equals("System.Security.Cryptography", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A cryptographic provider failure occurred during encryption.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Byte sequence could not be decoded into valid UTF-8 text.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("XML content could not be encoded into UTF-8 Byte sequence.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Compression stream failed during GZip operation.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("File system access failed while reading XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("XML processing entered an invalid operational state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The provided input type is not supported by the compression algorithm.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An attempt was made to use a disposed stream or reader.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Insufficient memory available to complete compression or encryption.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Access to the XML resource was denied by the operating system.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("XML parsing failed due to malformed or invalid XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during XML compression and encryption.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToEncryptedBytesExtensions
{
    public static Byte[] CompressXmlToEncryptedBytes(this String input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this StringBuilder input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XmlDocument input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XmlElement input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XmlNode input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XDocument input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XElement input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this XmlReader input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this Stream input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this MemoryStream input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this TextReader input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this FileInfo input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this Uri input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this Byte[] input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToEncryptedBytes(this Object input)
    {
        return CompressInternal(input);
    }
}