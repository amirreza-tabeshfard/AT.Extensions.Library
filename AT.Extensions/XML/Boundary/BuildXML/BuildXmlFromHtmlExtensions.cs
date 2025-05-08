namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromHtmlExtensions
    : Object
{
    public static System.Xml.Linq.XDocument BuildXmlFromHtml(this String htmlContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XCData(htmlContent)));
            return doc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content is invalid or empty.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("HTML content format is not valid for XML conversion.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML building.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("System.Xml.Linq.XDocument"))
        {
            throw new InvalidOperationException("XDocument object was disposed during XML building.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Out of memory while building XML document.", ex);
        }
        catch (SystemException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("A system error occurred during XML processing.", ex);
        }
        catch (Exception ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML document.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromHtml(this String htmlContent, String rootElementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement(rootElementName, new System.Xml.Linq.XCData(htmlContent)));
            return doc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("Root element name parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("Root element name parameter is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("The document cannot have more than one root element."))
        {
            throw new InvalidOperationException("The XML document structure is invalid due to multiple root elements.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("An error occurred while parsing EntityName."))
        {
            throw new InvalidOperationException("Invalid character found in HTML content.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Unexpected end of file has occurred."))
        {
            throw new InvalidOperationException("The HTML content is incomplete and cannot be converted to XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Unexpected token."))
        {
            throw new InvalidOperationException("The HTML content contains unexpected tokens and is not valid for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Invalid character in the given encoding."))
        {
            throw new InvalidOperationException("The HTML content contains invalid characters based on the current encoding.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Root element is missing."))
        {
            throw new InvalidOperationException("The HTML content does not generate a valid XML structure because the root element is missing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Data at the root level is invalid. Line 1, position 1."))
        {
            throw new InvalidOperationException("The HTML content is not properly formatted to be converted to XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Equals("Input string was not in a correct format."))
        {
            throw new InvalidOperationException("The HTML content has an invalid format.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("There is not enough memory available to build the XML document.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access denied while building the XML document.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security violation occurred while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromHtml(this String htmlContent, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] encodedContent = encoding.GetBytes(htmlContent);
            String decodedContent = encoding.GetString(encodedContent);
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XCData(decodedContent)));
            return doc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("Encoding parameter is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("Encoding parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode the HTML content using the specified encoding.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to encode the HTML content using the specified encoding.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("The document cannot have more than one root element."))
        {
            throw new InvalidOperationException("The XML document structure is invalid due to multiple root elements.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("An error occurred while parsing EntityName."))
        {
            throw new InvalidOperationException("Invalid character found in HTML content during XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Unexpected end of file has occurred."))
        {
            throw new InvalidOperationException("The HTML content is incomplete and cannot be converted to XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Unexpected token."))
        {
            throw new InvalidOperationException("The HTML content contains unexpected tokens and is not valid for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Invalid character in the given encoding."))
        {
            throw new InvalidOperationException("The HTML content contains invalid characters based on the specified encoding.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Root element is missing."))
        {
            throw new InvalidOperationException("The HTML content does not generate a valid XML structure because the root element is missing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Data at the root level is invalid. Line 1, position 1."))
        {
            throw new InvalidOperationException("The HTML content is not properly formatted to be converted to XML.", ex);
        }
        catch (FormatException ex) when (ex.Message.Equals("Input string was not in a correct format."))
        {
            throw new InvalidOperationException("The HTML content format is invalid.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("There is not enough memory available to build the XML document.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access denied while building the XML document.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security violation occurred while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromHtml(this String htmlContent, String rootElementName, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] encodedContent = encoding.GetBytes(htmlContent);
            String decodedContent = encoding.GetString(encodedContent);
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement(rootElementName, new System.Xml.Linq.XCData(decodedContent)));
            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(htmlContent)))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new InvalidOperationException("Root element name parameter is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(encoding)))
        {
            throw new InvalidOperationException("Encoding parameter is null.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode HTML content with the provided encoding.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to encode HTML content with the provided encoding.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("System ran out of memory while building the XML document.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Generated XML content is invalid.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Content format is invalid and cannot be processed into XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Internal invalid operation occurred while building the XML.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document.", ex);
        }
    }

    public static String BuildXmlFromHtml(this String htmlContent, Boolean minifyOutput)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XCData(htmlContent)));
            String xmlString = doc.ToString();
            return minifyOutput ? xmlString.Replace(Environment.NewLine, string.Empty).Replace("  ", string.Empty) : xmlString;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode HTML content.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to encode HTML content.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Content format is invalid and cannot be converted to XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while generating XML from HTML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing XML content.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The generated XML is invalid.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred during XML processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML string from HTML.", ex);
        }
    }

    public static String BuildXmlFromHtml(this String htmlContent, Int32 indentation)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("root", new System.Xml.Linq.XCData(htmlContent)));
            System.Xml.XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = new String(' ', indentation),
                OmitXmlDeclaration = true
            };
            using StringWriter sw = new();
            using System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sw, settings);
            doc.Save(xw);
            return sw.ToString();
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(indentation)) && (indentation < 0))
        {
            throw new InvalidOperationException("Indentation parameter cannot be negative.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("indentation"))
        {
            throw new InvalidOperationException("Indentation parameter is invalid.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("indentation"))
        {
            throw new InvalidOperationException("Indentation cannot be negative.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the XML content.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while building the indented XML string from HTML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing XML content.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred during XML processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building indented XML string from HTML.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromHtml(this String htmlContent, String rootElementName, Boolean wrapInCData)
    {
        ArgumentException.ThrowIfNullOrEmpty(htmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(htmlContent);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (wrapInCData)
                return new System.Xml.Linq.XElement(rootElementName, new System.Xml.Linq.XCData(htmlContent));
            else
                return new System.Xml.Linq.XElement(rootElementName, htmlContent);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContent"))
        {
            throw new InvalidOperationException("HTML content parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("Root element name parameter is invalid.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while processing the XML content.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while building XElement from HTML.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("A required parameter is null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the HTML content is incorrect.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing XML content.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred during XML processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XElement from HTML.", ex);
        }
    }

    public static List<System.Xml.Linq.XElement> BuildXmlFromHtml(this IEnumerable<String> htmlContents)
    {
        ArgumentNullException.ThrowIfNull(htmlContents);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            List<System.Xml.Linq.XElement> elements = new();
            foreach (String? html in htmlContents)
            {
                if (String.IsNullOrWhiteSpace(html))
                    continue;

                elements.Add(new System.Xml.Linq.XElement("item", new System.Xml.Linq.XCData(html)));
            }
            return elements;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContents"))
        {
            throw new InvalidOperationException("HTML contents collection is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContents"))
        {
            throw new InvalidOperationException("HTML contents collection is empty or contains invalid entries.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("html"))
        {
            throw new InvalidOperationException("HTML content in the collection is null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of one or more HTML contents is invalid.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while building XML elements from HTML contents.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing HTML contents.", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new InvalidOperationException("An argument was out of range while processing HTML contents.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred during XML element generation from HTML contents.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the list of XML elements from HTML contents.", ex);
        }
    }

    public static List<System.Xml.Linq.XElement> BuildXmlFromHtml(this IEnumerable<String> htmlContents, String itemElementName)
    {
        ArgumentNullException.ThrowIfNull(htmlContents);
        ArgumentException.ThrowIfNullOrEmpty(itemElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(itemElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            List<System.Xml.Linq.XElement> elements = new();
            foreach (String? html in htmlContents)
            {
                if (String.IsNullOrWhiteSpace(html))
                    continue;

                elements.Add(new System.Xml.Linq.XElement(itemElementName, new System.Xml.Linq.XCData(html)));
            }
            return elements;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("htmlContents"))
        {
            throw new InvalidOperationException("HTML contents collection is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("itemElementName"))
        {
            throw new InvalidOperationException("Item element name is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("itemElementName"))
        {
            throw new InvalidOperationException("Item element name is empty or contains only whitespace.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the HTML content is invalid.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while building XML elements from HTML contents with the custom element name.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing HTML contents.", ex);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new InvalidOperationException("An argument was out of range while processing HTML contents.", ex);
        }
        catch (SystemException ex)
        {
            throw new InvalidOperationException("A system error occurred during XML element generation from HTML contents.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the list of XML elements from HTML contents with the custom element name.", ex);
        }
    }
}