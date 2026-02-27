using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToGzipBytesExtensions
{
    private static Byte[] CompressXmlInternal(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            var xmlString = String.Empty;

            if (input is String s)
                xmlString = s;
            else if (input is XDocument xd)
                xmlString = xd.Declaration != null ? xd.Declaration + xd.ToString() : xd.ToString();
            else if (input is XElement xe)
                xmlString = xe.ToString();
            else if (input is MemoryStream ms)
            {
                ms.Position = 0;
                using var reader = new StreamReader(ms, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is Stream st)
            {
                st.Position = 0;
                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else if (input is FileInfo fi)
            {
                using var reader = fi.OpenText();
                xmlString = reader.ReadToEnd();
            }
            else if (input is Uri uri)
            {
                using var client = new System.Net.Http.HttpClient();
                var result = client.GetStringAsync(uri).Result;
                xmlString = result ?? throw new InvalidOperationException("Failed to read content from Uri.");
            }
            else if (input is Byte[] bytes)
                xmlString = Encoding.UTF8.GetString(bytes);
            else if (input is XmlReader xr)
            {
                var doc = XDocument.Load(xr);
                xmlString = doc.ToString();
            }
            else if (input is XmlDocument xDoc)
            {
                using var stringWriter = new StringWriter();
                using var xmlTextWriter = new XmlTextWriter(stringWriter);
                xDoc.WriteTo(xmlTextWriter);
                xmlString = stringWriter.ToString();
            }
            else if (input is XElement[] xeArray)
            {
                var sbArray = new StringBuilder();
                foreach (var element in xeArray)
                    sbArray.Append(element.ToString());
                xmlString = sbArray.ToString();
            }
            else if (input is XDocument[] xdArray)
            {
                var sbArray = new StringBuilder();
                foreach (var doc in xdArray)
                    sbArray.Append(doc.ToString());
                xmlString = sbArray.ToString();
            }
            else
            {
                xmlString = input.ToString();
                if (String.IsNullOrWhiteSpace(xmlString))
                    throw new InvalidCastException("The input Object cannot be converted to a valid XML String.");
            }

            var xmlBytes = Encoding.UTF8.GetBytes(xmlString);
            using var outputStream = new MemoryStream();
            using (var gzipStream = new GZipStream(outputStream, CompressionLevel.Optimal, true))
            {
                gzipStream.Write(xmlBytes, 0, xmlBytes.Length);
            }

            outputStream.Position = 0;
            return outputStream.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Compression failed because the input Object is null.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("cannot be converted to a valid XML String"))
        {
            throw new InvalidOperationException("Compression failed because the input Object cannot be converted to a valid XML String.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Failed to read content from Uri"))
        {
            throw new InvalidOperationException("Compression failed because reading content from the specified Uri failed.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to an I/O error while reading or writing streams.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Stream does not support"))
        {
            throw new InvalidOperationException("Compression failed because the provided stream does not support the required operations.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Compression failed because a stream Object was disposed before completion.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Message.Contains("Unable to translate"))
        {
            throw new InvalidOperationException("Compression failed due to an encoding error when converting XML to bytes.", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("response status code"))
        {
            throw new InvalidOperationException("Compression failed because fetching content from Uri resulted in an HTTP error.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed due to an unexpected error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToGzipBytesExtensions
{
    public static Byte[] CompressXmlToGzipBytes(this String xmlContent)
    {
        return CompressXmlInternal(xmlContent);
    }

    public static Byte[] CompressXmlToGzipBytes(this XDocument xDocument)
    {
        return CompressXmlInternal(xDocument);
    }

    public static Byte[] CompressXmlToGzipBytes(this XElement xElement)
    {
        return CompressXmlInternal(xElement);
    }

    public static Byte[] CompressXmlToGzipBytes(this MemoryStream memoryStream)
    {
        return CompressXmlInternal(memoryStream);
    }

    public static Byte[] CompressXmlToGzipBytes(this Stream stream)
    {
        return CompressXmlInternal(stream);
    }

    public static Byte[] CompressXmlToGzipBytes(this TextReader textReader)
    {
        return CompressXmlInternal(textReader);
    }

    public static Byte[] CompressXmlToGzipBytes(this StringBuilder stringBuilder)
    {
        return CompressXmlInternal(stringBuilder);
    }

    public static Byte[] CompressXmlToGzipBytes(this FileInfo fileInfo)
    {
        return CompressXmlInternal(fileInfo);
    }

    public static Byte[] CompressXmlToGzipBytes(this Uri uri)
    {
        return CompressXmlInternal(uri);
    }

    public static Byte[] CompressXmlToGzipBytes(this Byte[] byteArray)
    {
        return CompressXmlInternal(byteArray);
    }

    public static Byte[] CompressXmlToGzipBytes(this Object obj)
    {
        return CompressXmlInternal(obj);
    }

    public static Byte[] CompressXmlToGzipBytes(this XmlReader xmlReader)
    {
        return CompressXmlInternal(xmlReader);
    }

    public static Byte[] CompressXmlToGzipBytes(this XmlDocument xmlDocument)
    {
        return CompressXmlInternal(xmlDocument);
    }

    public static Byte[] CompressXmlToGzipBytes(this XElement[] xElements)
    {
        return CompressXmlInternal(xElements);
    }

    public static Byte[] CompressXmlToGzipBytes(this XDocument[] xDocuments)
    {
        return CompressXmlInternal(xDocuments);
    }
}