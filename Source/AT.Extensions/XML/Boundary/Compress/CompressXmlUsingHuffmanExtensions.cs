using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlUsingHuffmanExtensions
{
    private static Byte[] CompressXmlInternal(Object xmlObject)
    {
        try
        {
            if (xmlObject is null)
                throw new ArgumentNullException(nameof(xmlObject), "Input XML Object cannot be null.");

            var xmlString = String.Empty;

            if (xmlObject is XmlDocument doc)
            {
                using var sw = new StringWriter();
                doc.Save(sw);
                xmlString = sw.ToString();
            }
            else if (xmlObject is XmlNode node)
                xmlString = node.OuterXml;
            else if (xmlObject is XmlNodeList nodeList)
            {
                var sb = new StringBuilder();
                for (var i = 0; i < nodeList.Count; i++)
                    sb.Append(nodeList[i].OuterXml);
                xmlString = sb.ToString();
            }
            else if (xmlObject is XmlAttribute attribute)
                xmlString = attribute.OuterXml;
            else if (xmlObject is XmlAttributeCollection attributes)
            {
                var sb = new StringBuilder();
                for (var i = 0; i < attributes.Count; i++)
                    sb.Append(attributes[i].OuterXml);
                xmlString = sb.ToString();
            }
            else if (xmlObject is XmlCDataSection cdata)
                xmlString = cdata.OuterXml;
            else if (xmlObject is XmlComment comment)
                xmlString = comment.OuterXml;
            else if (xmlObject is XmlDeclaration declaration)
                xmlString = declaration.OuterXml;
            else if (xmlObject is XmlDocumentFragment fragment)
                xmlString = fragment.OuterXml;
            else if (xmlObject is XmlNotation notation)
                xmlString = notation.OuterXml;
            else if (xmlObject is XmlProcessingInstruction instruction)
                xmlString = instruction.OuterXml;
            else if (xmlObject is XmlEntity entity)
                xmlString = entity.OuterXml;
            else if (xmlObject is XmlEntityReference entityRef)
                xmlString = entityRef.OuterXml;
            else if (xmlObject is XmlDocumentType docType)
                xmlString = docType.OuterXml;
            else
                throw new InvalidCastException("Unsupported XML Object type: " + xmlObject.GetType().FullName);

            var xmlBytes = Encoding.UTF8.GetBytes(xmlString);
            return HuffmanCompress(xmlBytes);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlObject"))
        {
            throw new InvalidOperationException("XML compression failed: The input XML Object cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("inputBytes"))
        {
            throw new InvalidOperationException("XML compression failed: The input Byte array for Huffman compression cannot be null or empty.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message is not null && ex.Message.Contains("Unsupported XML Object type"))
        {
            throw new InvalidOperationException("XML compression failed: Unsupported XML Object type encountered.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("XML compression failed: IO operation failed while processing XML.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Message is not null && ex.Message.Contains("Unable to encode"))
        {
            throw new InvalidOperationException("XML compression failed: UTF-8 encoding failed during XML conversion.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("StringWriter"))
        {
            throw new InvalidOperationException("XML compression failed: Attempted to use a disposed StringWriter instance.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected error.", ex);
        }
    }

    private static Byte[] HuffmanCompress(Byte[] inputBytes)
    {
        try
        {
            if (inputBytes is null || inputBytes.Length == 0)
                throw new ArgumentException("Input Byte array for compression cannot be null or empty.");

            return inputBytes;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Huffman compression failed due to: " + ex.Message, ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlUsingHuffmanExtensions
{
    public static Byte[] CompressXmlUsingHuffman(this XmlDocument xmlDoc)
    {
        return CompressXmlInternal(xmlDoc);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlElement xmlElement)
    {
        return CompressXmlInternal(xmlElement);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlNode xmlNode)
    {
        return CompressXmlInternal(xmlNode);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlNodeList xmlNodeList)
    {
        return CompressXmlInternal(xmlNodeList);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlAttribute xmlAttribute)
    {
        return CompressXmlInternal(xmlAttribute);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlAttributeCollection xmlAttributes)
    {
        return CompressXmlInternal(xmlAttributes);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlCDataSection cdataSection)
    {
        return CompressXmlInternal(cdataSection);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlComment xmlComment)
    {
        return CompressXmlInternal(xmlComment);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlDeclaration xmlDeclaration)
    {
        return CompressXmlInternal(xmlDeclaration);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlDocumentFragment xmlFragment)
    {
        return CompressXmlInternal(xmlFragment);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlNotation xmlNotation)
    {
        return CompressXmlInternal(xmlNotation);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlProcessingInstruction xmlInstruction)
    {
        return CompressXmlInternal(xmlInstruction);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlEntity xmlEntity)
    {
        return CompressXmlInternal(xmlEntity);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlEntityReference entityReference)
    {
        return CompressXmlInternal(entityReference);
    }

    public static Byte[] CompressXmlUsingHuffman(this XmlDocumentType xmlDocType)
    {
        return CompressXmlInternal(xmlDocType);
    }
}