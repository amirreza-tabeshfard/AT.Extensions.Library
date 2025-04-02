namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromBsonExtenstions
    : Object
{
    #region Private Method(s)

    private static String FormatXml(System.Xml.XmlDocument xmlDocument)
    {
        ArgumentNullException.ThrowIfNull(xmlDocument);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlWriterSettings settings = new()
        {
            Indent = true,
            NewLineOnAttributes = true
        };
        using StringWriter stringWriter = new();
        using System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringWriter, settings);
        xmlDocument.Save(xmlWriter);
        // ----------------------------------------------------------------------------------------------------
        return stringWriter.ToString();
    }

    #endregion

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument)
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }

        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains invalid format data.", ex);
        }

        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document structure is invalid for XML conversion.", ex);
        }

        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'Root' element is missing"))
        {
            throw new InvalidOperationException("XML Document could not be created due to missing root element.", ex);
        }

        catch (Exception ex)
        {
            throw new InvalidOperationException("Error while converting BSON to XML.", ex);
        }
    }

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument, String rootElementName = "Root")
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("must not be whitespace"))
        {
            throw new InvalidOperationException("The root element name cannot contain only whitespace characters.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains invalid format data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document structure is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'Root' element is missing"))
        {
            throw new InvalidOperationException("XML Document could not be created due to missing root element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The BSON document contains invalid characters that cannot be represented in XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting BSON to XML.", ex);
        }
    }

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument, String rootElementName, String attributeName)
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                xmlElement.SetAttribute(attributeName, "true");
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("The attribute name cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("The attribute name cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("must not be whitespace"))
        {
            throw new InvalidOperationException("The root element name or attribute name cannot contain only whitespace characters.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains invalid format data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document structure is invalid for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'Root' element is missing"))
        {
            throw new InvalidOperationException("XML Document could not be created due to missing root element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The BSON document contains invalid characters that cannot be represented in XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the character"))
        {
            throw new InvalidOperationException("The root element name or attribute name contains an invalid starting character for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot contain whitespace characters"))
        {
            throw new InvalidOperationException("The attribute name contains whitespace, which is not allowed in XML attributes.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting BSON to XML.", ex);
        }
    }

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument, bool formatted)
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);

            return formatted ? FormatXml(xmlDocument) : xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains an invalid format that cannot be converted to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document has an invalid structure for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The BSON document contains invalid characters that cannot be represented in XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the character"))
        {
            throw new InvalidOperationException("The BSON document contains an element name that starts with an invalid character for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot contain whitespace characters"))
        {
            throw new InvalidOperationException("An element name in the BSON document contains whitespace, which is not allowed in XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("A null reference occurred during BSON to XML conversion, possibly due to missing data.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("Unable to translate Unicode character"))
        {
            throw new InvalidOperationException("The BSON document contains characters that cannot be encoded in XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting BSON to XML.", ex);
        }
    }

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument, List<String> fields)
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        ArgumentNullException.ThrowIfNull(fields);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement("Root");
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             where fields.Contains(element.Name)
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("fields"))
        {
            throw new InvalidOperationException("The fields list cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Value cannot be an empty collection"))
        {
            throw new InvalidOperationException("The fields list cannot be empty.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("A null reference occurred during BSON to XML conversion, possibly due to missing fields.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary"))
        {
            throw new InvalidOperationException("A field specified in the list does not exist in the BSON document.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains an invalid format that cannot be converted to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document has an invalid structure for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The BSON document contains invalid characters that cannot be represented in XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the character"))
        {
            throw new InvalidOperationException("The BSON document contains an element name that starts with an invalid character for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot contain whitespace characters"))
        {
            throw new InvalidOperationException("An element name in the BSON document contains whitespace, which is not allowed in XML.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("Unable to translate Unicode character"))
        {
            throw new InvalidOperationException("The BSON document contains characters that cannot be encoded in XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting BSON to XML.", ex);
        }
    }

    public static String BuildXmlFromBson(this MongoDB.Bson.BsonDocument bsonDocument, String rootElementName, List<String> fieldsToInclude)
    {
        ArgumentNullException.ThrowIfNull(bsonDocument);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(fieldsToInclude);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDocument = new();
            System.Xml.XmlElement rootElement = xmlDocument.CreateElement(rootElementName);
            
            foreach ((MongoDB.Bson.BsonElement element, System.Xml.XmlElement xmlElement) in from MongoDB.Bson.BsonElement element in bsonDocument.Elements
                                                                                             where fieldsToInclude.Contains(element.Name)
                                                                                             let xmlElement = xmlDocument.CreateElement(element.Name)
                                                                                             select (element, xmlElement))
            {
                xmlElement.InnerText = element.Value.ToString();
                rootElement.AppendChild(xmlElement);
            }

            xmlDocument.AppendChild(rootElement);
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bsonDocument"))
        {
            throw new InvalidOperationException("The BSON document cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("fieldsToInclude"))
        {
            throw new InvalidOperationException("The fieldsToInclude list cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("empty"))
        {
            throw new InvalidOperationException("The root element name cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && ex.Message.Contains("whitespace"))
        {
            throw new InvalidOperationException("The root element name cannot contain only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("fieldsToInclude") && ex.Message.Contains("empty"))
        {
            throw new InvalidOperationException("The fieldsToInclude list cannot be empty.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("The given key was not present in the dictionary"))
        {
            throw new InvalidOperationException("A field specified in fieldsToInclude does not exist in the BSON document.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("The BSON document contains an invalid format that cannot be converted to XML.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document is not well-formed"))
        {
            throw new InvalidOperationException("The BSON document has an invalid structure for XML conversion.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("The BSON document contains invalid characters that cannot be represented in XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot begin with the character"))
        {
            throw new InvalidOperationException("The BSON document contains an element name that starts with an invalid character for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Name cannot contain whitespace characters"))
        {
            throw new InvalidOperationException("An element name in the BSON document contains whitespace, which is not allowed in XML.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("Unable to translate Unicode character"))
        {
            throw new InvalidOperationException("The BSON document contains characters that cannot be encoded in XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting BSON to XML.", ex);
        }
    }
}