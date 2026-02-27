using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithCustomAlgorithmExtensions
{
    private static String HandleCompression(Object xmlObject)
    {
        try
        {
            if (xmlObject is null)
                throw new ArgumentNullException(nameof(xmlObject), "Input XML Object cannot be null");

            var xmlString = String.Empty;

            if (xmlObject is XmlDocument doc)
                xmlString = doc.OuterXml;
            else if (xmlObject is XmlNode node)
                xmlString = node.OuterXml;
            else if (xmlObject is String str)
                xmlString = str;
            else
                throw new InvalidCastException("Unsupported XML type provided for compression");

            var bytes = Encoding.UTF8.GetBytes(xmlString);
            var compressedBytes = SimpleCompressionAlgorithm(bytes);
            return Convert.ToBase64String(compressedBytes);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("data"))
        {
            throw new InvalidOperationException($"Compression algorithm failed because input data is null or empty. Parameter name: {ex.ParamName}", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlObject"))
        {
            throw new InvalidOperationException($"Compression algorithm failed because input XML Object is null. Parameter name: {ex.ParamName}", ex);
        }
        catch (InvalidCastException ex) when (ex.Message is not null && ex.Message.Contains("Unsupported XML type"))
        {
            throw new InvalidOperationException("Compression algorithm failed because an unsupported XML type was provided", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Message is not null && ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Compression algorithm failed due to an encoding error during XML to Byte conversion", ex);
        }
        catch (FormatException ex) when (ex.Message is not null && ex.Message.Contains("Base64"))
        {
            throw new InvalidOperationException("Compression algorithm failed because the compressed data could not be converted to Base64", ex);
        }
        catch (OverflowException ex) when (ex.Message is not null && ex.Message.Contains("Byte"))
        {
            throw new InvalidOperationException("Compression algorithm failed because a Byte overflow occurred during compression", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"XML compression failed due to an unexpected error. Source: {(ex.Source is not null ? ex.Source : "Unknown")}", ex);
        }
    }

    private static Byte[] SimpleCompressionAlgorithm(Byte[] data)
    {
        if (data is null || data.Length == 0)
            throw new ArgumentException("Data for compression cannot be null or empty", nameof(data));

        var compressed = new Byte[data.Length];
        for (var i = 0; i < data.Length; i++)
            compressed[i] = (Byte)(data[i] ^ 0xAA);

        return compressed;
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlWithCustomAlgorithmExtensions
{
    public static String CompressXmlWithCustomAlgorithm(this XmlDocument xmlDocument)
    {
        return HandleCompression(xmlDocument);
    }

    public static String CompressXmlWithCustomAlgorithm(this String xmlString)
    {
        return HandleCompression(xmlString);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlNode xmlNode)
    {
        return HandleCompression(xmlNode);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlElement xmlElement)
    {
        return HandleCompression(xmlElement);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlAttribute xmlAttribute)
    {
        return HandleCompression(xmlAttribute);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlNodeList xmlNodeList)
    {
        return HandleCompression(xmlNodeList);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlDocumentFragment xmlFragment)
    {
        return HandleCompression(xmlFragment);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlDeclaration xmlDeclaration)
    {
        return HandleCompression(xmlDeclaration);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlComment xmlComment)
    {
        return HandleCompression(xmlComment);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlCDataSection cdataSection)
    {
        return HandleCompression(cdataSection);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlProcessingInstruction processingInstruction)
    {
        return HandleCompression(processingInstruction);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlDocumentType documentType)
    {
        return HandleCompression(documentType);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlEntityReference entityReference)
    {
        return HandleCompression(entityReference);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlNotation notation)
    {
        return HandleCompression(notation);
    }

    public static String CompressXmlWithCustomAlgorithm(this XmlWhitespace whitespace)
    {
        return HandleCompression(whitespace);
    }
}