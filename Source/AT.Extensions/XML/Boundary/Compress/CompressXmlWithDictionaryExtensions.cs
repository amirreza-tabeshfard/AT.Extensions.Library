using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlWithDictionaryExtensions
{
    private static String CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException("Input cannot be null.");

            if (input is XmlDocument xmlDoc)
                return CompressXml(xmlDoc.OuterXml);
            else if (input is XmlElement xmlElement)
                return CompressXml(xmlElement.OuterXml);
            else if (input is XmlNode xmlNode)
                return CompressXml(xmlNode.OuterXml);
            else if (input is XmlAttribute xmlAttribute)
                return CompressXml(xmlAttribute.OuterXml);
            else if (input is XmlNodeList xmlNodeList)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < xmlNodeList.Count; i++)
                    builder.Append(xmlNodeList[i].OuterXml);
                return CompressXml(builder.ToString());
            }
            else if (input is String xmlString)
            {
                if (String.IsNullOrWhiteSpace(xmlString))
                    throw new ArgumentException("Input String cannot be empty or whitespace.");
                return CompressXml(xmlString);
            }
            else if (input is StringBuilder xmlBuilder)
            {
                if (xmlBuilder.Length == 0)
                    throw new ArgumentException("Input StringBuilder cannot be empty.");
                return CompressXml(xmlBuilder.ToString());
            }
            else if (input is Dictionary<String, String> xmlDict)
            {
                var builder = new StringBuilder();
                foreach (var kvp in xmlDict)
                    builder.Append($"<{kvp.Key}>{kvp.Value}</{kvp.Key}>");
                return CompressXml(builder.ToString());
            }
            else if (input is List<XmlNode> nodeList)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < nodeList.Count; i++)
                    builder.Append(nodeList[i].OuterXml);
                return CompressXml(builder.ToString());
            }
            else if (input is XmlQualifiedName qualifiedName)
                return CompressXml($"<{qualifiedName.Name}>{qualifiedName.Namespace}</{qualifiedName.Name}>");
            else if (input is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.Load(xmlReader);
                return CompressXml(doc.OuterXml);
            }
            else if (input is XmlWriter)
                throw new NotSupportedException("Cannot compress XmlWriter directly. Provide XmlDocument or XmlNode instead.");
            else if (input is XmlDocumentFragment xmlFragment)
                return CompressXml(xmlFragment.OuterXml);
            else
                throw new ArgumentException($"Unsupported input type: {input.GetType().FullName}");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Invalid argument provided for input. Parameter: {ex.ParamName}", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Input String cannot be empty or whitespace. Parameter: {ex.ParamName}", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlBuilder"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Input StringBuilder cannot be empty. Parameter: {ex.ParamName}", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Input cannot be null. Parameter: {ex.ParamName}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: XML operation failed in System.Xml.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Equals("Cannot compress XmlWriter directly. Provide XmlDocument or XmlNode instead."))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Cannot compress XmlWriter directly. Use XmlDocument or XmlNode instead.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException($"Compression failed. Reason: Invalid XML format encountered during parsing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression failed. Reason: An unexpected error occurred. Source: {(ex.Source is not null ? ex.Source : "Unknown")}", ex);
        }
    }

    private static String CompressXml(String xml)
    {
        var doc = new XmlDocument();
        doc.LoadXml(xml);
        using var stringWriter = new StringWriter();
        using var xmlTextWriter = new XmlTextWriter(stringWriter)
        {
            Formatting = Formatting.None
        };
        doc.WriteTo(xmlTextWriter);
        return stringWriter.ToString();
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 14 )
/// </summary>
public static partial class CompressXmlWithDictionaryExtensions
{
    public static String CompressXmlWithDictionary(this XmlDocument xmlDoc)
    {
        return CompressInternal(xmlDoc);
    }

    public static String CompressXmlWithDictionary(this XmlElement xmlElement)
    {
        return CompressInternal(xmlElement);
    }

    public static String CompressXmlWithDictionary(this XmlNode xmlNode)
    {
        return CompressInternal(xmlNode);
    }

    public static String CompressXmlWithDictionary(this XmlAttribute xmlAttribute)
    {
        return CompressInternal(xmlAttribute);
    }

    public static String CompressXmlWithDictionary(this XmlNodeList xmlNodeList)
    {
        return CompressInternal(xmlNodeList);
    }

    public static String CompressXmlWithDictionary(this String xmlString)
    {
        return CompressInternal(xmlString);
    }

    public static String CompressXmlWithDictionary(this StringBuilder xmlBuilder)
    {
        return CompressInternal(xmlBuilder);
    }

    public static String CompressXmlWithDictionary(this Dictionary<String, String> xmlDictionary)
    {
        return CompressInternal(xmlDictionary);
    }

    public static String CompressXmlWithDictionary(this List<XmlNode> xmlNodeList)
    {
        return CompressInternal(xmlNodeList);
    }

    public static String CompressXmlWithDictionary(this XmlQualifiedName qualifiedName)
    {
        return CompressInternal(qualifiedName);
    }

    public static String CompressXmlWithDictionary(this XmlReader xmlReader)
    {
        return CompressInternal(xmlReader);
    }

    public static String CompressXmlWithDictionary(this XmlWriter xmlWriter)
    {
        return CompressInternal(xmlWriter);
    }

    public static String CompressXmlWithDictionary(this XmlDocumentFragment xmlFragment)
    {
        return CompressInternal(xmlFragment);
    }

    public static String CompressXmlWithDictionary(this Object unknownXml)
    {
        return CompressInternal(unknownXml);
    }
}