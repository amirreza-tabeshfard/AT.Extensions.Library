namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromExcelExtenstions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<String> excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((String item, System.Xml.XmlElement element) in from item in excelData
                                                                     let element = xmlDoc.CreateElement("Item")
                                                                     select (item, element))
            {
                element.InnerText = item;
                root.AppendChild(element);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null)
        {
            throw new InvalidOperationException($"The parameter {ex.ParamName} cannot be null while building XML from Excel data.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException("An error occurred while processing the XML data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("An error occurred while building XML from Excel data"))
        {
            throw new InvalidOperationException("There was an issue during the XML construction process, likely due to invalid data format.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid format"))
        {
            throw new InvalidOperationException("The Excel data contains an invalid format that cannot be processed into XML.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Equals("Value was either too large or too small for an Int32."))
        {
            throw new InvalidOperationException("The Excel data contains numeric values that are out of range for processing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this Dictionary<String, String> excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((KeyValuePair<string, string> entry, System.Xml.XmlElement element) in from entry in excelData
                                                                                            let element = xmlDoc.CreateElement(entry.Key)
                                                                                            select (entry, element))
            {
                element.InnerText = entry.Value;
                root.AppendChild(element);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new InvalidOperationException("Excel data dictionary is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("name"))
        {
            throw new InvalidOperationException("Element name derived from dictionary key is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("Failed to set element inner text due to invalid value.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.Equals(string.Empty))
        {
            throw new InvalidOperationException("Invalid XML structure or format encountered.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Failed to append element to the XML document.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Attempted to access a null reference in the XML creation process.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("One of the dictionary values is in an incorrect format.", ex);
        }
        catch (System.Text.EncoderFallbackException ex) when (ex.Message.Contains("Unable to translate"))
        {
            throw new InvalidOperationException("Encoding issue encountered while writing XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<Int32> excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);

            foreach ((Int32 item, System.Xml.XmlElement element) in from int item in excelData
                                                                    let element = xmlDoc.CreateElement("Item")
                                                                    select (item, element))
            {
                element.InnerText = item.ToString();
                root.AppendChild(element);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentNullException("Input list 'excelData' is null.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new InvalidOperationException("XML element creation failed due to null reference.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("AppendChild"))
        {
            throw new InvalidOperationException("Appending XML element failed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Root"))
        {
            throw new InvalidOperationException("Error occurred while creating the root XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Item"))
        {
            throw new InvalidOperationException("Error occurred while creating the item XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("InnerText"))
        {
            throw new InvalidOperationException("Setting inner text of XML element failed.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("ToString"))
        {
            throw new InvalidOperationException("Failed to convert item to string for XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException("XML structure is not valid at the specified line.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.SourceUri is not null && ex.SourceUri.EndsWith(".xml"))
        {
            throw new InvalidOperationException("XML error related to source document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<IEnumerable<String>> excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((IEnumerable<String> row, System.Xml.XmlElement rowElement) in from IEnumerable<String> row in excelData
                                                                                    let rowElement = xmlDoc.CreateElement("Row")
                                                                                    select (row, rowElement))
            {
                foreach ((String item, System.Xml.XmlElement element) in from string item in row
                                                                         let element = xmlDoc.CreateElement("Item")
                                                                         select (item, element))
                {
                    element.InnerText = item;
                    rowElement.AppendChild(element);
                }

                root.AppendChild(rowElement);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new InvalidOperationException("Excel data source cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("name"))
        {
            throw new InvalidOperationException("An invalid element name was provided while creating XML nodes.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The node to be inserted is from a different document context"))
        {
            throw new InvalidOperationException("XML node insertion failed due to document context mismatch.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("This document already has a 'DocumentElement' node"))
        {
            throw new InvalidOperationException("Only one root element is allowed in the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("is not a valid XML name"))
        {
            throw new InvalidOperationException("One or more XML node names are invalid.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new InvalidOperationException("A null reference was encountered during XML generation.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new InvalidOperationException("A format issue occurred while processing Excel data into XML.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("Collection was modified; enumeration operation may not execute"))
        {
            throw new InvalidOperationException("The collection was modified during enumeration, causing XML build to fail.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<Tuple<String, String>> excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((Tuple<String, String> item, System.Xml.XmlElement element) in from Tuple<String, String> item in excelData
                                                                                    let element = xmlDoc.CreateElement(item.Item1)
                                                                                    select (item, element))
            {
                element.InnerText = item.Item2;
                root.AppendChild(element);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentNullException("Input data cannot be null. Please ensure 'excelData' is properly initialized.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Collection was modified; enumeration operation may not execute."))
        {
            throw new InvalidOperationException("The collection was modified during enumeration. Ensure the data source remains unchanged while processing.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was detected in the element name or value. Check the input data for correctness.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new System.Xml.XmlException("Unexpected end of XML detected. Ensure all tags and elements are properly closed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("name cannot begin with"))
        {
            throw new System.Xml.XmlException("Element names cannot begin with invalid characters. Validate that the input keys are valid XML tag names.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("name"))
        {
            throw new ArgumentException("The element name provided is not valid. Ensure all keys in the input data are suitable XML tag names.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new NullReferenceException("An object reference was null. Ensure all necessary objects are instantiated before use.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new FormatException("Invalid format detected in the input data. Please verify that all values conform to expected formats.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("access denied"))
        {
            throw new Exception("Access to a required resource was denied. Check the application's permissions.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this String excelData, Int32 version)
    {
        ArgumentException.ThrowIfNullOrEmpty(excelData);
        ArgumentException.ThrowIfNullOrWhiteSpace(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);

            System.Xml.XmlElement dataElement = xmlDoc.CreateElement("Data");
            dataElement.InnerText = excelData;
            root.AppendChild(dataElement);

            System.Xml.XmlElement versionElement = xmlDoc.CreateElement("Version");
            versionElement.InnerText = version.ToString();
            root.AppendChild(versionElement);

            return xmlDoc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentException("The input string 'excelData' is null or empty. Please provide a valid string.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("cannot be white space"))
        {
            throw new ArgumentException("The input string 'excelData' cannot be whitespace. Provide meaningful content.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new FormatException("The version value could not be converted to string properly. Ensure it has a valid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("The document already has a 'DocumentElement' node"))
        {
            throw new InvalidOperationException("The XML document already contains a root element. Ensure no duplicate root is added.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was found in the XML structure. Ensure element names and values are valid.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new System.Xml.XmlException("Unexpected end of XML content. Ensure all XML nodes are correctly formed and closed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("name cannot begin with"))
        {
            throw new System.Xml.XmlException("Element name starts with an invalid character. Make sure your XML tags start with a valid letter or underscore.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("is an invalid name"))
        {
            throw new System.Xml.XmlException("One of the element names in the XML is invalid. Check the naming of XML nodes.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference not set to an instance of an object"))
        {
            throw new NullReferenceException("A required object was null. Verify that all XML elements are properly initialized before use.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("access denied"))
        {
            throw new Exception("Access to a required resource was denied. Check the application permissions or XML handling restrictions.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this String[] excelData)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((String item, System.Xml.XmlElement element) in from String item in excelData
                                                                     let element = xmlDoc.CreateElement("Item")
                                                                     select (item, element))
            {
                element.InnerText = item;
                root.AppendChild(element);
            }

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentNullException("The input array 'excelData' is null. Ensure it is initialized properly before calling this method.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentException("The input 'excelData' contains an invalid argument.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid format"))
        {
            throw new FormatException("An element in the input 'excelData' has an invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Cannot create"))
        {
            throw new InvalidOperationException("An internal error occurred while creating XML elements.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("malformed"))
        {
            throw new System.Xml.XmlException("The XML structure being created is malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was found in the XML content.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new System.Xml.XmlException($"An XML error occurred at line {ex.LineNumber}.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference"))
        {
            throw new NullReferenceException("A null object was accessed during XML building process.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("The system ran out of memory while building the XML document.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("unexpected"))
        {
            throw new SystemException("An unexpected system error occurred during XML creation.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new Exception("An error occurred while creating an XML element from the input data.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this FileInfo excelFile)
    {
        ArgumentNullException.ThrowIfNull(excelFile);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);

            System.Xml.XmlElement element = xmlDoc.CreateElement("FileName");
            element.InnerText = excelFile.Name;
            root.AppendChild(element);

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelFile"))
        {
            throw new ArgumentNullException("The parameter 'excelFile' is null. Please provide a valid FileInfo object.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelFile"))
        {
            throw new ArgumentException("The parameter 'excelFile' is invalid. Ensure it references a valid file path.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(excelFile.FullName))
        {
            throw new FileNotFoundException("The specified file in 'excelFile' was not found.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access"))
        {
            throw new UnauthorizedAccessException("Access to the specified file is denied. Check file permissions.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("being used"))
        {
            throw new IOException("The file is currently in use by another process.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("I/O"))
        {
            throw new IOException("An I/O error occurred while accessing the file.", ex);
        }
        catch (PathTooLongException ex) when (ex.Message.Contains("too long"))
        {
            throw new PathTooLongException("The file path specified in 'excelFile' is too long.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was found while generating XML content.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("malformed"))
        {
            throw new System.Xml.XmlException("The structure of the generated XML is malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new System.Xml.XmlException($"An XML error occurred at line {ex.LineNumber}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new InvalidOperationException("An error occurred while creating an XML element.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("The system ran out of memory while building the XML document.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("Name"))
        {
            throw new Exception("An error occurred while reading the file name from 'excelFile'.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<String> excelData, String fileName)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        ArgumentException.ThrowIfNullOrEmpty(fileName);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((String item, System.Xml.XmlElement element) in from String item in excelData
                                                                     let element = xmlDoc.CreateElement("Item")
                                                                     select (item, element))
            {
                element.InnerText = item;
                root.AppendChild(element);
            }

            System.Xml.XmlElement fileElement = xmlDoc.CreateElement("FileName");
            fileElement.InnerText = fileName;
            root.AppendChild(fileElement);

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentNullException("The parameter 'excelData' is null. Please provide a valid list of strings.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("fileName"))
        {
            throw new ArgumentNullException("The parameter 'fileName' is null. Please provide a valid file name.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("fileName"))
        {
            throw new ArgumentException("The parameter 'fileName' is either empty or whitespace. Provide a non-empty name.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid format"))
        {
            throw new FormatException("An item in 'excelData' has an invalid format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new InvalidOperationException("An error occurred while creating XML elements from the input list.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("enumeration"))
        {
            throw new InvalidOperationException("An error occurred during enumeration of the 'excelData' list.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was detected while generating XML elements.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("malformed"))
        {
            throw new System.Xml.XmlException("The XML document being created is malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new System.Xml.XmlException($"An XML error occurred at line {ex.LineNumber}.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference"))
        {
            throw new NullReferenceException("A null object was referenced during XML building process.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("The system ran out of memory while processing the input data or creating the XML document.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("FileName"))
        {
            throw new Exception("An error occurred while assigning the file name to the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Excel data.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromExcel(this IEnumerable<Int32> excelData, Int32 version)
    {
        ArgumentNullException.ThrowIfNull(excelData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            System.Xml.XmlElement root = xmlDoc.CreateElement("Root");
            xmlDoc.AppendChild(root);
            
            foreach ((Int32 item, System.Xml.XmlElement element) in from Int32 item in excelData
                                                                    let element = xmlDoc.CreateElement("Item")
                                                                    select (item, element))
            {
                element.InnerText = item.ToString();
                root.AppendChild(element);
            }

            System.Xml.XmlElement versionElement = xmlDoc.CreateElement("Version");
            versionElement.InnerText = version.ToString();
            root.AppendChild(versionElement);

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("excelData"))
        {
            throw new ArgumentNullException("The parameter 'excelData' is null. Please provide a valid sequence of integers.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input string was not in a correct format"))
        {
            throw new FormatException("An item in 'excelData' could not be converted to string format.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("CreateElement"))
        {
            throw new InvalidOperationException("An error occurred while creating XML elements for items in the list.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("enumeration"))
        {
            throw new InvalidOperationException("An error occurred during enumeration of the 'excelData' sequence.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new System.Xml.XmlException("An invalid character was detected while generating the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("malformed"))
        {
            throw new System.Xml.XmlException("The XML structure being built is malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new System.Xml.XmlException($"An XML error occurred at line {ex.LineNumber}.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("Object reference"))
        {
            throw new NullReferenceException("A null object was referenced during the XML creation process.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new OutOfMemoryException("The system ran out of memory while building the XML document.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("value was either too large"))
        {
            throw new OverflowException("An overflow occurred while converting integer values to strings in XML.", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("Version"))
        {
            throw new Exception("An error occurred while appending the version element to the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while building XML from Excel data.", ex);
        }
    }
}