namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromMultipleDictionariesExtenstions
    : Object
{
    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, String> dict)
    {
        ArgumentNullException.ThrowIfNull(dict);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (KeyValuePair<String, String> item in dict)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' cannot be null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"Argument '{ex.ParamName}' is out of range.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null)
        {
            throw new InvalidOperationException($"The operation could not be completed due to invalid state or condition: {ex.Source}.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("A null reference was encountered during XML generation.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("An XML generation error occurred due to invalid XML format.", ex);
        }
        catch (IOException ex) when (ex.Message is not null)
        {
            throw new InvalidOperationException("An I/O error occurred while handling the XML operation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from dictionary", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, String> dict, String additionalElement)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentException.ThrowIfNullOrEmpty(additionalElement);
        ArgumentException.ThrowIfNullOrWhiteSpace(additionalElement);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new System.Xml.Linq.XElement("Root");

            foreach (KeyValuePair<String, String> item in dict)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            xml.Add(new System.Xml.Linq.XElement("AdditionalElement", additionalElement));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new ArgumentNullException("Argument cannot be null. Check the input parameters.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("additionalElement"))
        {
            throw new ArgumentException("The additionalElement argument cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict"))
        {
            throw new ArgumentException("The dictionary cannot be null or empty.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML. Please verify the XML structure or data types.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the dictionary and additional element.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, String> dict1, Dictionary<String, String> dict2)
    {
        ArgumentNullException.ThrowIfNull(dict1);
        ArgumentNullException.ThrowIfNull(dict2);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (KeyValuePair<String, String> item in dict1)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            foreach (KeyValuePair<String, String> item in dict2)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict1"))
        {
            throw new ArgumentNullException("The first dictionary cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict2"))
        {
            throw new ArgumentNullException("The second dictionary cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML. Please verify the XML structure or data types.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from multiple dictionaries.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, String> dict, IEnumerable<String> list)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentNullException.ThrowIfNull(list);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (KeyValuePair<String, String> item in dict)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            foreach (String item in list)
                xml.Add(new System.Xml.Linq.XElement("Item", item));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict"))
        {
            throw new ArgumentNullException("The dictionary cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list"))
        {
            throw new ArgumentNullException("The list cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML. Please verify the XML structure or data types.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list"))
        {
            throw new ArgumentException("The list cannot be empty or contain null values.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the dictionary and list.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, String> dict, IEnumerable<KeyValuePair<String, String>> kvpList)
    {
        ArgumentNullException.ThrowIfNull(dict);
        ArgumentNullException.ThrowIfNull(kvpList);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (KeyValuePair<String, String> item in dict)
                xml.Add(new System.Xml.Linq.XElement(item.Key, item.Value));

            foreach (KeyValuePair<String, String> kvp in kvpList)
                xml.Add(new System.Xml.Linq.XElement(kvp.Key, kvp.Value));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict"))
        {
            throw new ArgumentNullException("The dictionary cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("kvpList"))
        {
            throw new ArgumentNullException("The key-value pair list cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML. Please verify the XML structure or data types.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("kvpList"))
        {
            throw new ArgumentException("The key-value pair list cannot be empty or contain null values.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the dictionary and key-value pair list.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(Dictionary<String, Dictionary<String, String>> dict)
    {
        ArgumentNullException.ThrowIfNull(dict);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach ((KeyValuePair<String, Dictionary<String, String>> outerItem, System.Xml.Linq.XElement outerElement) in from KeyValuePair<String, Dictionary<String, String>> outerItem in dict
                                                                                                                            let outerElement = new System.Xml.Linq.XElement(outerItem.Key)
                                                                                                                            select (outerItem, outerElement))
            {
                foreach (KeyValuePair<String, String> innerItem in outerItem.Value)
                    outerElement.Add(new System.Xml.Linq.XElement(innerItem.Key, innerItem.Value));

                xml.Add(outerElement);
            }

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict"))
        {
            throw new ArgumentNullException("The dictionary cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML structure. Please verify the structure or data types.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Linq"))
        {
            throw new KeyNotFoundException("A key was not found in the dictionary during XML generation.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dict"))
        {
            throw new ArgumentException("The dictionary cannot be empty or contain null values.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from nested dictionaries.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(IEnumerable<String> list1, IEnumerable<String> list2)
    {
        ArgumentNullException.ThrowIfNull(list1);
        ArgumentNullException.ThrowIfNull(list2);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (String item in list1)
                xml.Add(new System.Xml.Linq.XElement("Item", item));

            foreach (String item in list2)
                xml.Add(new System.Xml.Linq.XElement("Item", item));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list1"))
        {
            throw new ArgumentNullException("The first list cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list2"))
        {
            throw new ArgumentNullException("The second list cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML structure. Please verify the structure or data types.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list1"))
        {
            throw new ArgumentException("The first list cannot be empty or contain null values.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list2"))
        {
            throw new ArgumentException("The second list cannot be empty or contain null values.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from two lists.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(IEnumerable<String> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (String item in list)
                xml.Add(new System.Xml.Linq.XElement("Item", item));

            return xml.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list"))
        {
            throw new ArgumentNullException("The provided list cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list"))
        {
            throw new ArgumentException("The provided list cannot be empty or contain invalid values.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while working with the XML elements.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while processing the XML structure. Please verify the data and structure.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the list.", ex);
        }
    }

    public static String BuildXmlFromMultipleDictionaries(IEnumerable<String> list, String additionalElement)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentException.ThrowIfNullOrEmpty(additionalElement);
        ArgumentException.ThrowIfNullOrWhiteSpace(additionalElement);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement xml = new("Root");

            foreach (String item in list)
                xml.Add(new System.Xml.Linq.XElement("Item", item));

            xml.Add(new System.Xml.Linq.XElement("AdditionalElement", additionalElement));

            return xml.ToString();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("additionalElement"))
        {
            throw new ArgumentException("The additional element must not be null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("list"))
        {
            throw new ArgumentNullException("The provided list cannot be null.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new FormatException("The format of the data added to the XML is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An error occurred while generating the XML structure.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new NullReferenceException("A null reference was encountered while creating XML elements.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new System.Security.SecurityException("A security error occurred during XML processing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new System.Xml.XmlException("An XML-related error occurred while constructing the document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from list and additional element.", ex);
        }
    }
}