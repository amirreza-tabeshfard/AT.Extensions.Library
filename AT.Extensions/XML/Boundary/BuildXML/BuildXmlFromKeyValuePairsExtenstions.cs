namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromKeyValuePairsExtenstions
    : Object
{
    public static String BuildXmlFromKeyValuePairs(this Dictionary<String, String> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (keyValuePairs == null)
                throw new ArgumentNullException(nameof(keyValuePairs), "Input dictionary cannot be null.");

            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);

            foreach ((KeyValuePair<String, String> pair, System.Xml.XmlElement node) in from KeyValuePair<String, String> pair in keyValuePairs
                                                                                        let node = xmlDoc.CreateElement(pair.Key)
                                                                                        select (pair, node))
            {
                node.InnerText = pair.Value;
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new ArgumentNullException("The input dictionary cannot be null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null)
        {
            throw new InvalidOperationException("Failed to create or manipulate XML document due to an XML error.", ex);
        }
        catch (InvalidOperationException ex) when (ex.HelpLink is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the XML structure.", ex);
        }
        catch (FormatException ex) when (ex.HResult.Equals(-2146233033))
        {
            throw new FormatException("A format error occurred while processing key or value for XML.", ex);
        }
        catch (System.Text.EncoderFallbackException ex)
        {
            throw new InvalidOperationException("An encoding error occurred while creating the XML String.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("The operation ran out of memory while building the XML String.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("name"))
        {
            throw new ArgumentException("A key provided in the dictionary is not a valid XML element name.", ex);
        }
        catch (NullReferenceException ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("A null reference occurred during XML element creation.", ex);
        }
        catch (Exception ex) when (ex.TargetSite is not null)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this List<Tuple<String, String>> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);

            foreach ((Tuple<String, String> pair, System.Xml.XmlElement node) in from Tuple<String, String> pair in keyValuePairs
                                                                                 let node = xmlDoc.CreateElement(pair.Item1)
                                                                                 select (pair, node))
            {
                node.InnerText = pair.Item2;
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new InvalidOperationException("The input list of key-value pairs is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("An argument provided to the XML element is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML element creation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML error occurred while constructing the XML document.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null reference was encountered during XML building.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A format exception occurred while processing key-value pairs.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The operation ran out of memory while building the XML document.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A non-supported operation was attempted during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this Dictionary<String, Object> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((KeyValuePair<String, Object> pair, System.Xml.XmlElement node) in from KeyValuePair<String, Object> pair in keyValuePairs
                                                                                        let node = xmlDoc.CreateElement(pair.Key)
                                                                                        select (pair, node))
            {
                node.InnerText = pair.Value.ToString();
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new InvalidOperationException("The dictionary provided was null and could not be processed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("name"))
        {
            throw new InvalidOperationException("An invalid XML element name was provided from dictionary key.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML error occurred while constructing the document.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred while appending XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("A format error occurred while converting a value to String.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Encoding failed due to a fallback error during String building.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The operation failed due to insufficient memory while creating XML.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("A null reference was encountered during XML creation.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An unsupported operation was attempted while working with XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this List<KeyValuePair<String, String>> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((KeyValuePair<String, String> pair, System.Xml.XmlElement node) in from KeyValuePair<String, String> pair in keyValuePairs
                                                                                        let node = xmlDoc.CreateElement(pair.Key)
                                                                                        select (pair, node))
            {
                node.InnerText = pair.Value;
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("pair.Key"))
        {
            throw new InvalidOperationException("Invalid argument provided for the XML element name.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("pair.Value"))
        {
            throw new InvalidOperationException("Invalid argument provided for the XML element value.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new InvalidOperationException("The key-value pair list cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML operation failed due to invalid state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML operation is not supported in the current context.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Insufficient memory to complete XML processing.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A security error occurred while processing XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred while creating or modifying the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this IDictionary<String, String> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((KeyValuePair<String, String> pair, System.Xml.XmlElement node) in from KeyValuePair<String, String> pair in keyValuePairs
                                                                                        let node = xmlDoc.CreateElement(pair.Key)
                                                                                        select (pair, node))
            {
                node.InnerText = pair.Value;
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("pair.Key"))
        {
            throw new InvalidOperationException("Invalid XML element name provided in key.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new InvalidOperationException("The provided key-value pairs dictionary is null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML creation.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The operation is not supported during XML processing.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Insufficient memory while building the XML document.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Security error encountered while accessing XML features.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML structure encountered during processing.", ex);
        }
        catch (System.Xml.XPath.XPathException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XPath error occurred while interacting with XML data.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Unauthorized access during XML manipulation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this KeyValuePair<String, String> keyValuePair)
    {
        ArgumentNullException.ThrowIfNull(keyValuePair);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);

            System.Xml.XmlElement node = xmlDoc.CreateElement(keyValuePair.Key);
            node.InnerText = keyValuePair.Value;
            rootNode.AppendChild(node);

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("key"))
        {
            throw new ArgumentException("The key provided for the XML element is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value"))
        {
            throw new ArgumentException("The value provided for the XML element is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePair"))
        {
            throw new ArgumentNullException("The input KeyValuePair Object cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML operation is not valid in the current state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new NotSupportedException("The operation is not supported by the XML DOM implementation.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new OutOfMemoryException("Insufficient memory to complete the XML creation process.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Security.SecurityException("Security restriction prevented XML processing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Xml.XmlException("An XML-specific error occurred while building the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this Dictionary<String, List<String>> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((KeyValuePair<String, List<String>> pair, System.Xml.XmlElement node) in from KeyValuePair<String, List<String>> pair in keyValuePairs
                                                                                              let node = xmlDoc.CreateElement(pair.Key)
                                                                                              select (pair, node))
            {
                foreach ((String value, System.Xml.XmlElement itemNode) in from String value in pair.Value
                                                                           let itemNode = xmlDoc.CreateElement("Item")
                                                                           select (value, itemNode))
                {
                    itemNode.InnerText = value;
                    node.AppendChild(itemNode);
                }

                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("pair.Key"))
        {
            throw new InvalidOperationException("The provided key is invalid or causes an XML element naming conflict.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("value"))
        {
            throw new InvalidOperationException("The provided value is invalid or cannot be used as XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML operation failed due to an invalid structure or misuse of XML APIs.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections"))
        {
            throw new InvalidOperationException("The dictionary does not contain the specified key.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("A null value was encountered while processing XML nodes.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An XML Object was disposed before usage.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Text"))
        {
            throw new InvalidOperationException("Insufficient memory while building the XML String.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An error occurred during XML processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this List<Dictionary<String, String>> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((Dictionary<String, String> dict, System.Xml.XmlElement subNode) in from Dictionary<String, String> dict in keyValuePairs
                                                                                         let subNode = xmlDoc.CreateElement("Item")
                                                                                         select (dict, subNode))
            {
                foreach ((KeyValuePair<String, String> pair, System.Xml.XmlElement node) in from KeyValuePair<String, String> pair in dict
                                                                                            let node = xmlDoc.CreateElement(pair.Key)
                                                                                            select (pair, node))
                {
                    node.InnerText = pair.Value;
                    subNode.AppendChild(node);
                }

                rootNode.AppendChild(subNode);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter '{ex.ParamName}' is null. Ensure that the list of key-value pairs is provided.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter '{ex.ParamName}' has an invalid value. Ensure that all dictionaries in the list are valid.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An XML error occurred while building the XML document. Ensure that the XML structure is valid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the key-value pairs.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("A format error occurred while building the XML. Check the data types of the key-value pairs.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("A null reference was encountered while processing the key-value pairs or XML document.", ex);
        }
        catch (OverflowException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("An overflow occurred during the processing of the XML. Ensure the values are within the acceptable range.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }

    public static String BuildXmlFromKeyValuePairs(this List<KeyValuePair<String, Object>> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Text.StringBuilder xmlStringBuilder = new();
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlNode rootNode = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(rootNode);
            
            foreach ((KeyValuePair<String, Object> pair, System.Xml.XmlElement node) in from KeyValuePair<String, Object> pair in keyValuePairs
                                                                                        let node = xmlDoc.CreateElement(pair.Key)
                                                                                        select (pair, node))
            {
                node.InnerText = pair.Value.ToString();
                rootNode.AppendChild(node);
            }

            return xmlDoc.OuterXml;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter '{ex.ParamName}' is null. Ensure that the list of key-value pairs is provided.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter '{ex.ParamName}' has an invalid value. Ensure that all key-value pairs in the list are valid.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("A format error occurred while building the XML. Check the data types of the key-value pairs.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the key-value pairs.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("A null reference was encountered while processing the key-value pairs or XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException("An XML error occurred while building the XML document. Ensure that the XML structure is valid.", ex);
        }
        catch (OverflowException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("An overflow occurred during the processing of the XML. Ensure the values are within the acceptable range.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Key-Value pairs.", ex);
        }
    }
}