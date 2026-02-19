using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlNormalizeExtensions
{
    private static String Synchronize(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input value cannot be null.");

            if (input is XmlDocument xmlDocument)
            {
                var doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.LoadXml(xmlDocument.OuterXml);
                return doc.OuterXml;
            }
            else if (input is XmlNode xmlNode)
            {
                var doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.LoadXml(xmlNode.OuterXml);
                return doc.OuterXml;
            }
            else if (input is XElement xElement)
                return xElement.ToString(SaveOptions.DisableFormatting);
            else if (input is XDocument xDocument)
                return xDocument.ToString(SaveOptions.DisableFormatting);
            else if (input is String xmlString)
            {
                var doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.LoadXml(xmlString);
                return doc.OuterXml;
            }
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                var doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.LoadXml(content);
                return doc.OuterXml;
            }
            else if (input is TextReader textReader)
            {
                var content = textReader.ReadToEnd();
                var doc = new XmlDocument
                {
                    PreserveWhitespace = false
                };
                doc.LoadXml(content);
                return doc.OuterXml;
            }
            else if (input is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.PreserveWhitespace = false;
                doc.Load(xmlReader);
                return doc.OuterXml;
            }
            else
                throw new InvalidOperationException("The provided input type is not supported for XML normalization.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentNullException(ex.ParamName, "The input argument was null. XML normalization requires a non-null reference type.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString"))
        {
            throw new ArgumentException("The provided XML string argument is invalid or empty.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred while processing the XML input.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while reading the XML stream or text source.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new NotSupportedException("The provided stream or reader does not support the required read operation.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ObjectDisposedException(ex.ObjectName, "The stream or reader was already disposed before XML normalization.");
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new UnauthorizedAccessException("Access to the XML stream source was denied during normalization.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("The XML content is malformed or violates XML syntax rules.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML normalization failed due to an unexpected runtime error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlNormalizeExtensions
{
    public static String CompressXmlNormalize(this XmlDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlNode input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlElement input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlAttribute input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlCDataSection input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlText input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlComment input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlProcessingInstruction input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XElement input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this XmlReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this TextReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this Stream input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this String input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlNormalize(this Object input)
    {
        return Synchronize(input);
    }
}