using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithOptimizedEncodingExtensions
{
    private static Byte[] ExecuteCompression(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            String xmlString = null;

            if (input is String s)
                xmlString = s;
            else if (input is XDocument xd)
                xmlString = xd.Declaration != null ? xd.Declaration + xd.ToString() : xd.ToString();
            else if (input is XElement xe)
                xmlString = xe.ToString();
            else if (input is XmlDocument xmld)
            {
                using var stringWriter = new StringWriter();
                using var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = false, Encoding = Encoding.UTF8 });
                xmld.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                xmlString = stringWriter.ToString();
            }
            else if (input is XmlNode xn)
                xmlString = xn.OuterXml;
            else if (input is String[] sa)
                xmlString = String.Join("", sa);
            else if (input is XDocument[] xda)
            {
                var builder = new StringBuilder();
                foreach (var doc in xda)
                    builder.Append(doc.ToString());
                xmlString = builder.ToString();
            }
            else if (input is XElement[] xea)
            {
                var builder = new StringBuilder();
                foreach (var el in xea)
                    builder.Append(el.ToString());
                xmlString = builder.ToString();
            }
            else if (input is XmlDocument[] xmlda)
            {
                var builder = new StringBuilder();
                foreach (var doc in xmlda)
                    builder.Append(doc.OuterXml);
                xmlString = builder.ToString();
            }
            else if (input is XmlNode[] xna)
            {
                var builder = new StringBuilder();
                foreach (var node in xna)
                    builder.Append(node.OuterXml);
                xmlString = builder.ToString();
            }
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else if (input is MemoryStream ms)
            {
                ms.Position = 0;
                using var reader = new StreamReader(ms, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xmlString = doc.OuterXml;
            }
            else
                throw new ArgumentException($"Unsupported input type: {input.GetType().FullName}", nameof(input));

            if (String.IsNullOrWhiteSpace(xmlString))
                throw new InvalidOperationException("The XML content is empty or whitespace only.");

            var xmlBytes = Encoding.UTF8.GetBytes(xmlString);
            using var compressedStream = new MemoryStream();
            using (var gzip = new GZipStream(compressedStream, CompressionLevel.Optimal, true))
            {
                gzip.Write(xmlBytes, 0, xmlBytes.Length);
            }
            return compressedStream.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed due to invalid argument: The input type is not supported. ParamName: {ex.ParamName}", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed due to null input: The input cannot be null. ParamName: {ex.ParamName}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("ExecuteCompression"))
        {
            throw new InvalidOperationException($"Compression failed due to invalid operation: {ex.Message}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("StreamReader"))
        {
            throw new InvalidOperationException($"Compression failed during stream reading: {ex.Message}", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("GZipStream"))
        {
            throw new InvalidOperationException($"Compression failed: The compression operation is not supported. Source: {ex.Source}", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("GZipStream"))
        {
            throw new InvalidOperationException($"Compression failed: Attempted to use a disposed compression stream. ObjectName: {ex.ObjectName}", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("UTF8Encoding"))
        {
            throw new InvalidOperationException($"Compression failed: UTF-8 encoding error occurred. Source: {ex.Source}", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("XmlDocument"))
        {
            throw new InvalidOperationException($"Compression failed: XML parsing error. Source: {ex.Source}, LineNumber: {ex.LineNumber}, LinePosition: {ex.LinePosition}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression failed due to unexpected error: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlWithOptimizedEncodingExtensions
{
    public static Byte[] CompressXmlWithOptimizedEncoding(this String xmlContent)
    {
        return ExecuteCompression(xmlContent);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XDocument xDocument)
    {
        return ExecuteCompression(xDocument);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XmlDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XElement xElement)
    {
        return ExecuteCompression(xElement);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this String[] xmlContents)
    {
        return ExecuteCompression(xmlContents);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XDocument[] xDocuments)
    {
        return ExecuteCompression(xDocuments);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XmlDocument[] xmlDocuments)
    {
        return ExecuteCompression(xmlDocuments);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XElement[] xElements)
    {
        return ExecuteCompression(xElements);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XmlNode[] xmlNodes)
    {
        return ExecuteCompression(xmlNodes);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this Stream xmlStream)
    {
        return ExecuteCompression(xmlStream);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this TextReader textReader)
    {
        return ExecuteCompression(textReader);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this StringBuilder stringBuilder)
    {
        return ExecuteCompression(stringBuilder);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this MemoryStream memoryStream)
    {
        return ExecuteCompression(memoryStream);
    }

    public static Byte[] CompressXmlWithOptimizedEncoding(this XmlReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }
}