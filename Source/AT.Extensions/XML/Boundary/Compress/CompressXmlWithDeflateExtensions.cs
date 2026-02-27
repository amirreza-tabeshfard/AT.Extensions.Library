using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithDeflateExtensions
{
    private static Byte[] ExecuteCompression(Object xmlObject)
    {
        try
        {
            if (xmlObject == null)
                throw new ArgumentNullException(nameof(xmlObject), "Input XML Object cannot be null.");

            String xmlString = null;

            if (xmlObject is XmlDocument doc)
                xmlString = doc.OuterXml;
            else if (xmlObject is XmlElement element)
                xmlString = element.OuterXml;
            else if (xmlObject is XmlNode node)
                xmlString = node.OuterXml;
            else if (xmlObject is XmlNodeList nodeList)
            {
                var sb = new StringBuilder();
                foreach (XmlNode nodeItem in nodeList)
                    if (nodeItem != null)
                        sb.Append(nodeItem.OuterXml);
                xmlString = sb.ToString();
            }
            else if (xmlObject is XmlAttribute attribute)
                xmlString = attribute.OuterXml;
            else if (xmlObject is XmlAttributeCollection attributes)
            {
                var sb = new StringBuilder();
                foreach (XmlAttribute attr in attributes)
                    if (attr != null)
                        sb.Append(attr.OuterXml);
                xmlString = sb.ToString();
            }
            else if (xmlObject is XmlCDataSection cdata)
                xmlString = cdata.OuterXml;
            else if (xmlObject is XmlComment comment)
                xmlString = comment.OuterXml;
            else if (xmlObject is XmlDeclaration declaration)
                xmlString = declaration.OuterXml;
            else if (xmlObject is XmlDocumentType docType)
                xmlString = docType.OuterXml;
            else if (xmlObject is XmlEntity entity)
                xmlString = entity.OuterXml;
            else if (xmlObject is XmlNotation notation)
                xmlString = notation.OuterXml;
            else if (xmlObject is XmlProcessingInstruction pi)
                xmlString = pi.OuterXml;
            else if (xmlObject is XmlSignificantWhitespace whitespace)
                xmlString = whitespace.OuterXml;
            else if (xmlObject is XmlText text)
                xmlString = text.OuterXml;
            else
                throw new InvalidOperationException($"Unsupported XML type: {xmlObject.GetType().FullName}");

            var bytes = Encoding.UTF8.GetBytes(xmlString);
            using var outputStream = new MemoryStream();
            using (var deflateStream = new DeflateStream(outputStream, CompressionLevel.Optimal, true))
            {
                deflateStream.Write(bytes, 0, bytes.Length);
            }
            return outputStream.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlObject"))
        {
            throw new InvalidOperationException("Compression failed: The input XML Object is null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("buffer"))
        {
            throw new InvalidOperationException("Compression failed: Buffer length is out of range during writing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlObject"))
        {
            throw new InvalidOperationException("Compression failed: The provided XML Object is invalid or cannot be processed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Compression failed: The XML type is unsupported by this method.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("DeflateStream"))
        {
            throw new InvalidOperationException("Compression failed: Attempted to write to a closed compression stream.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Compression failed: Not enough memory available to complete compression.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("Compression failed: IO error occurred during Deflate compression.", ex);
        }
        catch (EncoderFallbackException ex)
        {
            throw new InvalidOperationException("Compression failed: Encoding the XML to UTF-8 failed.", ex);
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
public static partial class CompressXmlWithDeflateExtensions
{
    public static Byte[] CompressXmlWithDeflate(this XmlDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlElement xmlElement)
    {
        return ExecuteCompression(xmlElement);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlNodeList xmlNodeList)
    {
        return ExecuteCompression(xmlNodeList);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlAttribute xmlAttribute)
    {
        return ExecuteCompression(xmlAttribute);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlAttributeCollection xmlAttributeCollection)
    {
        return ExecuteCompression(xmlAttributeCollection);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlCDataSection cDataSection)
    {
        return ExecuteCompression(cDataSection);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlComment xmlComment)
    {
        return ExecuteCompression(xmlComment);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlDeclaration xmlDeclaration)
    {
        return ExecuteCompression(xmlDeclaration);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlDocumentType xmlDocumentType)
    {
        return ExecuteCompression(xmlDocumentType);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlEntity xmlEntity)
    {
        return ExecuteCompression(xmlEntity);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlNotation xmlNotation)
    {
        return ExecuteCompression(xmlNotation);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlProcessingInstruction processingInstruction)
    {
        return ExecuteCompression(processingInstruction);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlSignificantWhitespace significantWhitespace)
    {
        return ExecuteCompression(significantWhitespace);
    }

    public static Byte[] CompressXmlWithDeflate(this XmlText xmlText)
    {
        return ExecuteCompression(xmlText);
    }
}
