using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToEncryptedStreamExtensions
{
    private static Stream Process(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            var xmlString = String.Empty;

            if (input is String s)
                xmlString = s;
            else if (input is XmlDocument xd)
                xmlString = xd.OuterXml;
            else if (input is XDocument xdoc)
                xmlString = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xe)
                xmlString = xe.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xmlString = doc.OuterXml;
            }
            else if (input is Stream st)
            {
                if (!st.CanRead)
                    throw new InvalidOperationException("Provided stream is not readable.");

                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is Byte[] bytes)
                xmlString = Encoding.UTF8.GetString(bytes);
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("XML file does not exist.", fi.FullName);

                xmlString = File.ReadAllText(fi.FullName, Encoding.UTF8);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file based URIs are supported.");

                xmlString = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
            }
            else if (input is XmlNode xn)
                xmlString = xn.OuterXml;
            else if (input is XmlAttribute xa)
                xmlString = xa.OuterXml;
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlString))
                throw new InvalidDataException("XML content is empty or whitespace.");

            var rawBytes = Encoding.UTF8.GetBytes(xmlString);

            using var compressedStream = new MemoryStream();
            using (var gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, true))
            {
                gzip.Write(rawBytes, 0, rawBytes.Length);
            }

            compressedStream.Position = 0;

            using var aes = Aes.Create();
            aes.Key = SHA256.HashData(Encoding.UTF8.GetBytes("AT.Xml.Boundary.Compress.Key"));
            aes.IV = new Byte[16];

            var encryptedStream = new MemoryStream();
            using (var crypto = new CryptoStream(encryptedStream, aes.CreateEncryptor(), CryptoStreamMode.Write, true))
            {
                compressedStream.CopyTo(crypto);
            }

            encryptedStream.Position = 0;
            return encryptedStream;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentException("The provided input argument is invalid for XML processing.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentNullException("The input reference was null and cannot be processed.", ex);
        }
        catch (CryptographicException ex) when (ex.Source is not null && ex.Source.Equals("System.Security.Cryptography"))
        {
            throw new CryptographicException("An error occurred during AES encryption initialization or execution.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidDataException("Failed to decode XML Byte content using UTF-8 encoding.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidDataException("Failed to encode XML content to UTF-8 Byte representation.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new FileNotFoundException("The specified XML file could not be found on disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O failure occurred while reading XML content or writing compressed data.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidDataException("The XML data was invalid, empty, or corrupted during compression.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("Compression failed due to an invalid operation in the compression stream.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Security.Cryptography"))
        {
            throw new InvalidOperationException("An invalid cryptographic operation was detected during encryption.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new NotSupportedException("The provided reference type is not supported for XML compression and encryption.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ObjectDisposedException("A stream required for XML processing was already disposed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new UnauthorizedAccessException("Access to the XML source or target stream was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("Malformed or invalid XML content was detected during parsing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while compressing and encrypting XML content.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToEncryptedStreamExtensions
{
    public static Stream CompressXmlToEncryptedStream(this String xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlDocument xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XDocument xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XElement xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlReader xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this Stream xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this TextReader xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this Byte[] xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this MemoryStream xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this FileInfo xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this Uri xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlNode xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlElement xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlAttribute xml)
    {
        return Process(xml);
    }

    public static Stream CompressXmlToEncryptedStream(this XmlText xml)
    {
        return Process(xml);
    }
}